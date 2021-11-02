using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SpaceInvaders.Model
{
    /// <summary>
    ///     Manages the entire game.
    /// </summary>
    public class GameManager
    {
        #region Types and Delegates

        /// <summary>
        /// Creates the score event handler.
        /// precondition: none
        /// post-condition: none
        /// </summary>
        /// <param name="count">The count.</param>
        public delegate void ScoreCountHandler(int count);

        /// <summary>
        /// Creates the game over event handler.
        /// precondition: none
        /// post-condition: none
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="title">The title.</param>
        public delegate void GameOverHandler(string content, string title);

        /// <summary>
        /// Creates the animation event handler.
        /// precondition: none
        /// post-condition: none
        /// </summary>
        public delegate void AnimationHandler();

        /// <summary>
        /// Creates the lives event handler.
        /// precondition: none
        /// post-condition: none
        /// </summary>
        /// <param name="lives">The lives.</param>
        public delegate void LivesCountHandler(int lives);

        #endregion

        #region Data members

        //private const int EnemyShipsPerRow = 4;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private PlayerShip playerShip;
        private IList<EnemyShip> enemyShips;

        private IList<ShipBullet> playerBullet;
        private IList<GameObject> enemyBullets;

        //private bool bulletFired;
        private bool enemyFired;

        private DispatcherTimer timer;
        private DispatcherTimer bulletTimer;

        private Canvas gameBackground;

        private readonly ShipsManager shipsManager;

        private int move;
        private int lives;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        ///     Precondition: backgroundHeight > 0 AND backgroundWidth > 0
        /// </summary>
        /// <param name="backgroundHeight">The backgroundHeight of the game play window.</param>
        /// <param name="backgroundWidth">The backgroundWidth of the game play window.</param>
        public GameManager(double backgroundHeight, double backgroundWidth)
        {
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
            this.shipsManager = new ShipsManager(this.backgroundHeight, this.backgroundWidth);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the game placing player ship and enemy ship in the game.
        ///     Precondition: background != null
        ///     Post-condition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="background">The background canvas.</param>
        public void InitializeGame(Canvas background)
        {
            if (background == null)
            {
                throw new ArgumentNullException(nameof(background));
            }

            this.shipsManager.InitializeShips(background);
            this.gameBackground = background;
            //this.bulletFired = false;
            this.enemyFired = false;
            this.enemyBullets = new List<GameObject>();
            this.playerBullet = new List<ShipBullet>();
            this.lives = 3;
            this.createAndPlacePlayerShip();

            this.createAndPlaceEnemyShips();

            this.initializeTimers();
        }

        private void initializeTimers()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 800);
            this.timer.Start();
            this.move = 1;

            this.bulletTimer = new DispatcherTimer();
            this.bulletTimer.Tick += this.bulletTimerTick;
            var rand = new Random();
            this.bulletTimer.Interval = new TimeSpan(0, 0, 0, 0, rand.Next(150, 250));
            this.bulletTimer.Start();
        }

        private void bulletTimerTick(object sender, object e)
        {
            //if (this.bulletFired)
            //{
            this.MoveBulletUp();
            this.EnemyDestroyed();
            //}

            if (this.enemyFired && this.enemyShips.Count != 0)
            {
                this.MoveBulletDown();
                this.PlayerDied();
            }

        }

        private void timerTick(object sender, object e)
        {
            if (this.move == (int)EnemyMoves.EnemyMove1)
            {
                this.MoveEnemyShipsLeft();
                this.changeShipLights();
            }

            if (this.move == (int)EnemyMoves.EnemyMove2)
            {
                this.MoveEnemyShipsRight();
                this.changeShipLights();
                this.OnAnimationUpdated();
            }

            if (this.move == (int)EnemyMoves.EnemyMove3)
            {
                this.MoveEnemyShipsRight();
                this.changeShipLights();
                this.OnAnimationUpdated();
            }

            if (this.move == (int)EnemyMoves.EnemyMove4)
            {
                this.MoveEnemyShipsLeft();
                this.changeShipLights();
                this.OnAnimationUpdated();
                this.move = 0;
            }

            this.move++;

            this.GetEnemyBulletsFired();

            //this.bulletFired = this.shipsManager.BulletFired;
        }

        private void changeShipLights()
        {
            foreach (var ship in this.enemyShips)
            {
                ship.Sprite.ChangeLightsColors();
            }
        }

        /// <summary>
        /// Creates the handler to handle the lives change
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        public event LivesCountHandler LivesCountUpdated;

        /// <summary>
        /// Defines what is handled when the lives is changed.
        /// Precondition: none
        /// Post-condition: the lives on screen should be updated if lives is changed.
        /// </summary>
        public void OnLivesCountUpdated()
        {
            this.LivesCountUpdated?.Invoke(this.lives);
        }

        /// <summary>
        /// Creates the handler to handle the score change
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        public event ScoreCountHandler ScoreCountUpdated;

        /// <summary>
        /// Defines what is handled when the score is changed.
        /// Precondition: none
        /// Post-condition: the score on screen should be updated if score is changed.
        /// </summary>
        public void OnScoreCountUpdated()
        {
            this.ScoreCountUpdated?.Invoke(this.Score);
        }

        /// <summary>
        /// Creates the handler to handle game over
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        public event GameOverHandler GameOverUpdated;

        /// <summary>
        /// Defines what is handled when the game is over.
        /// Precondition: none
        /// Post-condition: A dialog box should appear with the content for the game over condition.
        /// </summary>
        public void OnGameOverUpdated()
        {
            this.GameOverUpdated?.Invoke(this.Title, this.Content);
        }

        /// <summary>
        /// Creates the handler to handle the animation change
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        public event AnimationHandler AnimationUpdated;

        /// <summary>
        /// Defines what is handled when the animation is changed.
        /// Precondition: none
        /// Post-condition: the animation for each ship on screen should be updated if animation is changed.
        /// </summary>
        public void OnAnimationUpdated()
        {
            this.AnimationUpdated?.Invoke();
        }

        private void createAndPlacePlayerShip()
        {
            this.shipsManager.CreateAndPlacePlayerShip();
            this.playerShip = this.shipsManager.PlayerShip;
        }


        /// <summary>
        ///     Moves the player ship to the left.
        ///     Precondition: none
        ///     Post-condition: The player ship has moved left.
        /// </summary>
        public void MovePlayerShipLeft()
        {
            if (this.playerShip.X - this.playerShip.SpeedX > 0)
            {
                this.playerShip.MoveLeft();
            }
        }

        /// <summary>
        ///     Moves the player ship to the right.
        ///     Precondition: none
        ///     Post-condition: The player ship has moved right.
        /// </summary>
        public void MovePlayerShipRight()
        {
            if (this.playerShip.X + this.playerShip.Width + this.playerShip.SpeedX < this.backgroundWidth)
            {
                this.playerShip.MoveRight();
            }
        }
        private void createAndPlaceEnemyShips()
        {
            this.shipsManager.CreateAndPlaceEnemyShips();
            this.enemyShips = this.shipsManager.EnemyShips;
        }

        /// <summary>
        /// Moves the enemy ships to the left.
        /// Precondition: none
        /// Post-condition: The enemy ships have moved left.
        /// </summary>
        public void MoveEnemyShipsLeft()
        {
            foreach (var ship in this.enemyShips)
            {
                if (this.enemyShips.Count > 0)
                {
                    ship.MoveLeft();
                }
            }
        }

        /// <summary>
        /// Moves the enemy ships to the right.
        /// Precondition: none
        /// Post-condition: The enemy ship have moved right.
        /// </summary>
        public void MoveEnemyShipsRight()
        {
            foreach (var ship in this.enemyShips)
            {
                if (this.enemyShips.Count > 0)
                {
                    ship.MoveRight();
                }
            }
        }

        /// <summary>
        /// Creates and places a bullet as long as there isn't another on the screen.
        /// Precondition: none
        /// post-condition: bullet has been placed on the canvas or bullet already exists.
        /// </summary>
        public void CreateAndPlacePlayerShipBullet()
        {
            this.shipsManager.CreateAndPlacePlayerShipBullet();
            this.playerBullet = this.shipsManager.PlayerBullet;

        }

        /// <summary>
        /// Moves the player bullet up or remove if off screen
        /// Precondition: none
        /// Post-condition: The player bullet has moved up or has been removed
        /// </summary>
        public void MoveBulletUp()
        {
            ShipBullet shipBullet = null;
            foreach (var bullet in this.playerBullet)
            {
                if (bullet.Y + bullet.SpeedY < 0)
                {
                    this.gameBackground.Children.Remove(bullet.Sprite);
                    shipBullet = bullet;
                }

                bullet.MoveUp();
            }

            this.playerBullet.Remove(shipBullet);
            this.shipsManager.PlayerBullet = this.playerBullet;
        }

        /// <summary>
        /// Adds the enemy bullets fired to the canvas and list of bullets
        /// Precondition: none
        /// Post-condition: the bullets fired should be added to the background
        /// </summary>
        public void GetEnemyBulletsFired()
        {
            this.shipsManager.GetEnemyBulletsFired();
            this.enemyBullets = this.shipsManager.EnemyBullets;
            this.enemyFired = this.shipsManager.EnemyFired;
        }

        /// <summary>
        /// Moves each player bullet in the list down
        /// Precondition: none
        /// Post-condition: each bullet should be moved down
        /// </summary>
        public void MoveBulletDown()
        {
            for (int index = 0; index < this.enemyBullets.Count; index++)
            {
                if (this.enemyBullets[index].Y + this.enemyBullets[index].SpeedY > this.backgroundHeight)
                {
                    this.enemyBullets.Remove(this.enemyBullets[index]);
                }

                if (this.enemyBullets.Count > 0)
                {
                    this.enemyBullets[index].MoveDown();
                }
            }
        }

        /// <summary>
        /// Checks if the player was hit by a bullet.
        /// Precondition: none
        /// Post-condition: player ship should be removed if hit
        /// </summary>
        public void PlayerDied()
        {
            this.lives = this.shipsManager.PlayerDied();
            this.OnLivesCountUpdated();

            this.playerShip = this.shipsManager.PlayerShip;
            this.gameOver();
        }

        /// <summary>
        /// Checks if an enemy ship is hit by a bullet
        /// Precondition: none
        /// Post-condition: the enemy and bullet should be removed if the ship is hit
        /// </summary>
        public void EnemyDestroyed()
        {
            EnemyShip destroyedShip = this.shipsManager.EnemyDestroyed();
            //this.bulletFired = this.shipsManager.BulletFired;
            this.enemyShips = this.shipsManager.EnemyShips;

            if (destroyedShip != null)
            {
                this.updateScore(destroyedShip);
            }

            this.gameOver();
        }

        private void updateScore(EnemyShip destroyedShip)
        {
            this.Score += destroyedShip.Score;

            this.OnScoreCountUpdated();
        }

        private void gameOver()
        {

            if (this.enemyShips.Count == 0)
            {
                this.Title = "Congratulations, you won!";
                this.Content = "Score: " + this.Score;
                this.result();
                this.OnGameOverUpdated();
            }

            else if (!this.gameBackground.Children.Contains(this.playerShip.Sprite))
            {
                this.Title = "GameOver";
                this.Content = "You Died";
                this.result();
                this.OnGameOverUpdated();
            }
        }

        private void result()
        {
            this.timer.Stop();
            this.bulletTimer.Stop();

        }

        #endregion
    }
}