using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public delegate void ScoreCountHandler(int count);

        public delegate void GameOverHandler(string content, string title);

        #endregion

        #region Data members

        private const int EnemyShipsPerRow = 4;
        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private PlayerShip playerShip;
        private IList<EnemyShip> enemyShips;

        private ShipBullet playerBullet;
        private IList<GameObject> enemyBullets;

        private bool bulletFired;
        private bool enemyFired;

        private DispatcherTimer timer;
        private DispatcherTimer bulletTimer;

        private int move;

        private Canvas theCanvas;

        private readonly ShipsManager shipsManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the score.
        /// </summary>
        /// <value>
        ///     The score.
        /// </value>
        public int Score { get; set; }

        public string title { get; set; }

        public string content { get; set; }

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

            this.shipsManager.InitializeShips();
            this.theCanvas = background;
            this.bulletFired = false;
            this.enemyFired = false;
            this.enemyBullets = new List<GameObject>();
            this.playerBullet = new ShipBullet();
            this.createAndPlacePlayerShip(background);

            this.createAndPlaceEnemyShips(background);

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
            if (this.bulletFired)
            {
                this.MoveBulletUp();
                this.EnemyDestroyed(this.theCanvas);
            }

            if (this.enemyFired && this.enemyShips.Count != 0)
            {
                this.MoveBulletDown();
                this.PlayerDied(this.theCanvas);
            }
        
        }

        private void timerTick(object sender, object e)
        {
            if (this.move == (int)EnemyMoves.EnemyMove1)
            {
                this.MoveEnemyShipsLeft();
            }

            if (this.move == (int)EnemyMoves.EnemyMove2)
            {
                this.MoveEnemyShipsRight();
            }

            if (this.move == (int)EnemyMoves.EnemyMove3)
            {
                this.MoveEnemyShipsRight();
            }

            if (this.move == (int)EnemyMoves.EnemyMove4)
            {
                this.MoveEnemyShipsLeft();
                this.move = 0;
            }

            this.move++;

            this.GetEnemyBulletsFired(this.theCanvas);

            this.bulletFired = this.shipsManager.BulletFired;
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

        public event GameOverHandler GameOverUpdated;

        public void OnGameOverUpdated()
        {
            this.GameOverUpdated?.Invoke(this.title, this.content);
        }


        private void createAndPlacePlayerShip(Canvas background)
        {
            this.shipsManager.createAndPlacePlayerShip(background);
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
        private void createAndPlaceEnemyShips(Canvas background)
        {
            this.shipsManager.createAndPlaceEnemyShips(background);
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
        /// Precondition: background != null
        /// post-condition: bullet has been placed on the canvas or bullet already exists.
        /// </summary>
        /// <param name="background"></param>
        public void CreateAndPlacePlayerShipBullet(Canvas background)
        {
            this.shipsManager.CreateAndPlacePlayerShipBullet(background);
            this.playerBullet = this.shipsManager.PlayerBullet;

        }

        /// <summary>
        /// Moves the player bullet up or remove if off screen
        /// Precondition: none
        /// Post-condition: The player bullet has moved up or has been removed
        /// </summary>
        public void MoveBulletUp()
        {
            if (this.playerBullet.Y + this.playerBullet.SpeedY < 0)
            {
                this.theCanvas.Children.Remove(this.playerBullet.Sprite);
            }
            this.playerBullet.MoveUp();
        }

        /// <summary>
        /// Adds the enemy bullets fired to the canvas and list of bullets
        /// Precondition: background != null
        /// Post-condition: the bullets fired should be added to the background
        /// </summary>
        /// <param name="background">The canvas background</param>
        public void GetEnemyBulletsFired(Canvas background)
        {
            this.shipsManager.GetEnemyBulletsFired(background);
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
        /// Precondition: background != null
        /// Post-condition: player ship should be removed if hit
        /// </summary>
        /// <param name="background">the background canvas</param>
        public void PlayerDied(Canvas background)
        {
            this.shipsManager.PlayerDied(background);
            this.playerShip = this.shipsManager.PlayerShip;
            this.gameOver(background);
        }

        /// <summary>
        /// Checks if an enemy ship is hit by a bullet
        /// Precondition: background != null
        /// Post-condition: the enemy and bullet should be removed if the ship is hit
        /// </summary>
        /// <param name="background">The canvas background</param>
        public void EnemyDestroyed(Canvas background)
        {
            EnemyShip destroyedShip = this.shipsManager.EnemyDestroyed(background);
            this.bulletFired = this.shipsManager.BulletFired;
            this.enemyShips = this.shipsManager.EnemyShips;

            if (destroyedShip != null)
            {
                this.updateScore(destroyedShip);
            }

            this.gameOver(background);
        }

        private void updateScore(EnemyShip destroyedShip)
        {
            this.Score = destroyedShip.Score;

            this.OnScoreCountUpdated();
        }

        private void gameOver(Canvas background)
        {

            if (this.enemyShips.Count == 0)
            {
                this.title = "Congratulations, you won!";
                this.content = "Score: " + this.Score; 
                this.Result();
                this.OnGameOverUpdated();
            }

            else if (!background.Children.Contains(this.playerShip.Sprite))
            {
                this.title = "GameOver";
                this.content = "You Died";
                this.Result();
                this.OnGameOverUpdated();
            }
        }

        private void Result()
        {
            this.timer.Stop();
            this.bulletTimer.Stop();

        }

        /*private async void PlayerWon()
        {
            var playerWonDialog = new ContentDialog
            {
                Title = "Congratulations you won!",

                Content = "Score: " + this.Score,
                PrimaryButtonText = "Ok"
            };
            this.timer.Stop();
            this.bulletTimer.Stop();
            await playerWonDialog.ShowAsync();
        }*/

        #endregion
    }
}