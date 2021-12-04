using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.Model.Enemies;
using SpaceInvaders.Model.HighScoreBoard;
using SpaceInvaders.Model.Player;
using SpaceInvaders.View.Sprites;

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

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;

        private PlayerShip playerShip;
        private IList<EnemyShip> enemyShips;

        private DispatcherTimer timer;

        private Canvas gameBackground;

        private EnemyShipManager enemyShipManger;
        private PlayerShipManager playerShipManager;
        private BulletManager bulletManager;

        private int move;
        private int lives;
        private int currentRound;

        private const int finalRound = 3;
        private int count;
        private int powerUpsRemaining;

        private bool gotPowerUp;
        private IList<Shield> shields;

        private HighScoreBoardManager highScoreBoard;

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
        public string Result { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [right key down].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [right key down]; otherwise, <c>false</c>.
        /// </value>
        public bool RightKeyDown { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [left key down].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [left key down]; otherwise, <c>false</c>.
        /// </value>
        public bool LeftKeyDown { get; set; }

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
            this.LeftKeyDown = false;
            this.RightKeyDown = false;
            this.currentRound = 1;
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
            this.gameBackground = background ?? throw new ArgumentNullException(nameof(background));
            this.playerShipManager = new PlayerShipManager(this.gameBackground);
            this.enemyShipManger = new EnemyShipManager(this.gameBackground);
            this.bulletManager = new BulletManager(this.gameBackground);
            this.playerShipManager.InitializeShips();
            this.enemyShipManger.InitializeShips();
            this.highScoreBoard = new HighScoreBoardManager();
            this.lives = 3;
            this.gotPowerUp = false;
            this.shields = new List<Shield>();

            this.createAndPlacePlayerShip();

            this.createAndPlaceEnemyShips();

            this.initializeTimers();

            this.placeShields();
        }

        private void initializeTimers()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            this.timer.Start();
            this.move = 0;
            this.count = 1;
            this.powerUpsRemaining = 0;
        }


        private void timerTick(object sender, object e)
        {
            if (this.count % 4 == 0)
            {
                IList<int> moves = null;
                if (this.currentRound == 1)
                {
                    moves = new List<int> {1, 2, 2, 1};
                    if (moves[this.move] % 2 != 0)
                    {
                        this.enemyShipManger.MoveEnemyShipsLeft();

                    }
                    else
                    {
                        this.enemyShipManger.MoveEnemyShipsRight();
                    }
                }
                else if (this.currentRound == 2)
                {
                    this.enemyShipManger.MoveEnemySnipsLevel2();

                }
                else if (this.currentRound == 3)
                {
                    this.enemyShipManger.MoveEnemyShipsLevel3();
                }

                

                this.changeShipLights();
                this.OnAnimationUpdated();

                if (this.currentRound == 1)
                {
                    this.move++;
                    if (this.move >= moves.Count)
                    {
                        this.move = 0;
                    }
                }

                this.GetEnemyBulletsFired();
            }

            this.enemyShipManger.PlaceBonusShip();

            count += 1;
            this.bulletManager.MoveBulletUp();
            this.bulletManager.MovePowerUp();
            this.EnemyDestroyed();

            this.bulletManager.MoveBulletDown();
            this.PlayerDied();

            this.gameOver();

            if (this.LeftKeyDown)
            {
                this.MovePlayerShipLeft();
            }

            if (this.RightKeyDown)
            {
                this.MovePlayerShipRight();
            }

            this.checkForShieldCollisions();
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
        public event EventHandler PowerUpCountUpdated;

        /// <summary>
        /// Defines what is handled when the lives is changed.
        /// Precondition: none
        /// Post-condition: the lives on screen should be updated if lives is changed.
        /// </summary>
        public void OnPowerUpCountUpdated()
        {
            this.PowerUpCountUpdated?.Invoke(this.powerUpsRemaining, EventArgs.Empty);
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
        public event EventHandler GameOverUpdated;

        /// <summary>
        /// Defines what is handled when the game is over.
        /// Precondition: none
        /// Post-condition: A dialog box should appear with the content for the game over condition.
        /// </summary>
        public void OnGameOverUpdated()
        {
            this.GameOverUpdated?.Invoke(this, EventArgs.Empty);
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
            this.playerShipManager.CreateAndPlacePlayerShip();
            this.playerShip = this.playerShipManager.PlayerShip;
        }


        /// <summary>
        ///     Moves the player ship to the left.
        ///     Precondition: none
        ///     Post-condition: The player ship has moved left.
        /// </summary>
        public void MovePlayerShipLeft()
        {
            this.playerShipManager.MovePlayerShipLeft();

            //this.playerShipManager.MovePlayerShipLeft();

            /*if (this.playerShip.X - this.playerShip.SpeedX > 0)
            {
                this.playerShip.MoveLeft();
            }*/
        }

        /// <summary>
        ///     Moves the player ship to the right.
        ///     Precondition: none
        ///     Post-condition: The player ship has moved right.
        /// </summary>
        public void MovePlayerShipRight()
        {
            this.playerShipManager.MovePlayerShipRight();

            //this.playerShipManager.MovePlayerShipRight();

            /*if (this.playerShip.X + this.playerShip.Width + this.playerShip.SpeedX < this.backgroundWidth)
            {
                this.playerShip.MoveRight();
            }*/
        }
        private void createAndPlaceEnemyShips()
        {
            this.enemyShipManger.CreateAndPlaceEnemyShips();
            this.enemyShips = this.enemyShipManger.EnemyShips;
        }

        /// <summary>
        /// Creates and places a bullet as long as there isn't another on the screen.
        /// Precondition: none
        /// post-condition: bullet has been placed on the canvas or bullet already exists.
        /// </summary>
        public void CreateAndPlacePlayerShipBullet()
        {
            if (this.gotPowerUp)
            {
                this.bulletManager.CreateAndPlacePowerUp(this.playerShip);
                this.powerUpsRemaining -= 1;
                if (this.powerUpsRemaining == 0)
                {
                    this.gotPowerUp = false;
                }
            }
            else
            {
                this.bulletManager.CreateAndPlacePlayerShipBullet(this.playerShip);
            }
            //this.playerBullet = this.playerShipManager.PlayerBullets;

        }

        /// <summary>
        /// Adds the enemy bullets fired to the canvas and list of bullets
        /// Precondition: none
        /// Post-condition: the bullets fired should be added to the background
        /// </summary>
        public void GetEnemyBulletsFired()
        {
            this.bulletManager.GetEnemyBulletsFired(this.enemyShipManger.GetFiringEnemies(), this.playerShipManager.PlayerShip.X);
            //this.enemyBullets = this.enemyShipManger.EnemyBullets;
        }

        /// <summary>
        /// Checks if the player was hit by a bullet.
        /// Precondition: none
        /// Post-condition: player ship should be removed if hit
        /// </summary>
        public void PlayerDied()
        {
            //this.lives = this.shipsManager.PlayerDied();
            IDictionary<ShipBullet, int> result = this.playerShipManager.PlayerDied(this.bulletManager.EnemyBullets);
            foreach (var bullet in result.Keys)
            {
                this.bulletManager.RemoveEnemyBullet(bullet);
            }

            foreach (var liveValue in result.Values)
            {
                this.lives = liveValue;
            }
            this.OnLivesCountUpdated();

            //this.playerShip = this.shipsManager.PlayerShip;
            this.playerShip = this.playerShipManager.PlayerShip;
        }

        /// <summary>
        /// Checks if an enemy ship is hit by a bullet
        /// Precondition: none
        /// Post-condition: the enemy and bullet should be removed if the ship is hit
        /// </summary>
        public void EnemyDestroyed()
        {
            //EnemyShip destroyedShip = this.shipsManager.EnemyDestroyed();
            IDictionary<GameObject, EnemyShip> result = this.enemyShipManger.EnemyDestroyed(this.bulletManager.PlayerBullets);
            IDictionary<GameObject, EnemyShip> powerUpResult = this.enemyShipManger.EnemyDestroyed(this.bulletManager.PowerUps);

            //this.enemyShips = this.shipsManager.EnemyShips;
            this.enemyShips = this.enemyShipManger.EnemyShips;
            foreach (var bullet in result.Keys)
            {
                this.bulletManager.RemovePlayerBullet((ShipBullet)bullet);
                
            }

            foreach(var powerup in powerUpResult.Keys)
            {
                this.bulletManager.RemovePowerUp((PowerUp)powerup);
                this.powerUpsRemaining -= 1;
                this.OnPowerUpCountUpdated();
            }

            foreach (var destroyedShip in result.Values)
            {
                if (destroyedShip != null)
                {
                    this.updateScore(destroyedShip);
                    if (destroyedShip.Sprite is BonusEnemySprite)
                    {
                        this.gotPowerUp = true;
                        this.powerUpsRemaining += 7;
                        this.OnPowerUpCountUpdated();
                    }
                }
            }

        }

        private void updateScore(EnemyShip destroyedShip)
        {
            this.Score += destroyedShip.Score;

            this.OnScoreCountUpdated();
        }

        private void gameOver()
        {
            this.highScoreBoard.GetScoreAndLevel(this.Score, this.currentRound);
            if (this.enemyShips.Count == 0 && this.currentRound == finalRound)
            {
                SoundPlayer.PlaySound("gameOver.wav");
                this.Result = "win";
                this.result();
                this.OnGameOverUpdated();
            }

            else if (this.enemyShips.Count == 0 && this.currentRound != finalRound)
            {
                this.currentRound++;
                this.gameBackground.Children.Remove(this.playerShip.Sprite);
                this.clearBulletsFromGameBoard();
                this.clearShieldsFromGameBoard();
                this.timer.Stop();
                this.InitializeGame(this.gameBackground);
                this.setTimerIntervalForRound();
            }

            else if (!this.gameBackground.Children.Contains(this.playerShip.Sprite))
            {
                SoundPlayer.PlaySound("gameOver.wav");
                this.Result = "lose";
                this.result();
                this.OnGameOverUpdated();
            }

        }

        private void clearShieldsFromGameBoard()
        {
            var currentShields = this.shields;
            if (currentShields != null)
            {
                foreach (var currentShield in currentShields)
                {
                    this.gameBackground.Children.Remove(currentShield.Sprite);
                }
            }
        }

        private void setTimerIntervalForRound()
        {
            if (this.currentRound == 2)
            {
                this.timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            }
            else if (this.currentRound == 3)
            {
                this.timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            }
        }

        private void clearBulletsFromGameBoard()
        {
            foreach (var currentBullet in this.bulletManager.PlayerBullets)
            {
                this.gameBackground.Children.Remove(currentBullet.Sprite);
            }

            foreach (var currentBullet in this.bulletManager.EnemyBullets)
            {
                this.gameBackground.Children.Remove(currentBullet.Sprite);
            }
        }

        private void result()
        {
            this.timer.Stop();

        }

        private void placeShields()
        {
            double shieldX = 172.5;
            for (int i = 0; i <= 2; i++)
            {
                Shield shield = new Shield();
                shield.Y = 450;
                shield.X = shieldX;
                this.shields.Add(shield);
                shieldX += 222.5;
                this.gameBackground.Children.Add(shield.Sprite);
            }
            
        }

        private void checkForShieldCollisions()
        {
            IList<ShipBullet> playerBulletsToRemove = new List<ShipBullet>();
            IList<ShipBullet> enemyBulletsToRemove = new List<ShipBullet>();
            IList<Shield> shieldsToRemove = new List<Shield>();

            this.checkForPlayerBulletShieldCollision(playerBulletsToRemove, shieldsToRemove);
            this.checkForEnemyBulletShieldCollision(enemyBulletsToRemove, shieldsToRemove);
            this.checkForPlayerShipShieldCollision(shieldsToRemove);

            foreach (var currentBullet in playerBulletsToRemove)
            {
                this.bulletManager.PlayerBullets.Remove(currentBullet);
            }

            foreach (var currentBullet in enemyBulletsToRemove)
            {
                this.bulletManager.EnemyBullets.Remove(currentBullet);
            }

            foreach (var currentShield in shieldsToRemove)
            {
                this.shields.Remove(currentShield);
            }
        }

        private void checkForPlayerShipShieldCollision(IList<Shield> shieldsToRemove)
        {
            foreach (var currentShield in this.shields)
            {
                if (CollisionDetector.DetectCollision(currentShield, this.playerShipManager.PlayerShip))
                {
                    if (currentShield.HitsRemaining != 0)
                    {
                        currentShield.HitsRemaining--;
                        currentShield.Sprite.Opacity -= .25;
                        this.playerShipManager.PlayerCollidedWithShield();
                    }
                    else
                    {
                        this.gameBackground.Children.Remove(currentShield.Sprite);
                        shieldsToRemove.Add(currentShield);
                        this.playerShipManager.PlayerCollidedWithShield();
                    }
                }
            }
        }

        private void checkForEnemyBulletShieldCollision(IList<ShipBullet> enemyBulletsToRemove, IList<Shield> shieldsToRemove)
        {
            foreach (var currentBullet in this.bulletManager.EnemyBullets)
            {
                foreach (var currentShield in this.shields)
                {
                    if (CollisionDetector.DetectCollision(currentShield, currentBullet))
                    {
                        if (currentShield.HitsRemaining != 0)
                        {
                            currentShield.HitsRemaining--;
                            currentShield.Sprite.Opacity -= .25;
                            this.gameBackground.Children.Remove(currentBullet.Sprite);
                            enemyBulletsToRemove.Add(currentBullet);
                        }
                        else
                        {
                            this.gameBackground.Children.Remove(currentShield.Sprite);
                            this.gameBackground.Children.Remove(currentBullet.Sprite);
                            shieldsToRemove.Add(currentShield);
                            enemyBulletsToRemove.Add(currentBullet);
                        }
                    }
                }
            }
        }

        private void checkForPlayerBulletShieldCollision(IList<ShipBullet> playerBulletsToRemove, IList<Shield> shieldsToRemove)
        {
            foreach (var currentBullet in this.bulletManager.PlayerBullets)
            {
                foreach (var currentShield in this.shields)
                {
                    if (CollisionDetector.DetectCollision(currentShield, currentBullet))
                    {
                        if (currentShield.HitsRemaining != 0)
                        {
                            currentShield.HitsRemaining--;
                            currentShield.Sprite.Opacity -= .25;
                            this.gameBackground.Children.Remove(currentBullet.Sprite);
                            playerBulletsToRemove.Add((ShipBullet)currentBullet);
                        }
                        else
                        {
                            this.gameBackground.Children.Remove(currentShield.Sprite);
                            this.gameBackground.Children.Remove(currentBullet.Sprite);
                            shieldsToRemove.Add(currentShield);
                            playerBulletsToRemove.Add((ShipBullet)currentBullet);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Moves the player ship up.
        /// </summary>
        public void MovePlayerShipUp()
        {
            this.playerShipManager.MovePlayerShipUp();
        }

        /// <summary>
        /// Moves the player ship down.
        /// </summary>
        public void MovePlayerShipDown()
        {
            this.playerShipManager.MovePlayerShipDown();
        }

        #endregion
    }
}