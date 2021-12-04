using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.View.Sprites;

namespace SpaceInvaders.Model.Enemies
{
    /// <summary>
    ///     Manages the Enemy Ships
    /// </summary>
    class EnemyShipManager
    {

        #region DataMembers

        private int enemyShipsPerRow;
        private readonly Canvas gameBackground;
        private int level2StepCounter;
        private bool level2FirstStep;
        private int level2FirstStepCounter;
        private int level3StepCounter;
        private bool level3FirstStep;
        private int level3FirstStepCounter;

        //private BulletManager manager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the enemy ships.
        /// </summary>
        /// <value>
        ///     The enemy ships.
        /// </value>
        public IList<EnemyShip> EnemyShips { get; set; }

        /// <summary>
        ///     Gets or sets whether a bullet was fired.
        /// </summary>
        /// <value>
        ///     Weather a bullet was fired.
        /// </value>
        public bool BulletFired { get; set; }

        /// <summary>
        ///     Gets or sets whether the enemy fired.
        /// </summary>
        /// <value>
        ///     Whether an enemy fired..
        /// </value>
        public bool EnemyFired { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyShipManager"/> class.
        /// Precondition: none
        /// Post-conditon: none
        /// </summary>
        /// <param name="background">The background.</param>
        public EnemyShipManager(Canvas background)
        {
            this.gameBackground = background;
            //this.EnemyBullets = new List<ShipBullet>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the ships.
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        public void InitializeShips()
        {
            this.enemyShipsPerRow = 8;
            this.EnemyFired = false;
            this.BulletFired = false;
            this.level2StepCounter = 0;
            this.level2FirstStep = true;
            this.level2FirstStepCounter = 0;
            this.level3StepCounter = 0;
            this.level3FirstStep = true;
            this.level3FirstStepCounter = 0;
            //this.EnemyBullets = new List<ShipBullet>();
            //this.manager = new BulletManager(this.gameBackground);
        }

        /// <summary>
        /// Creates and places the enemy ships.
        /// Precondition: none
        /// Post-condition: enemy ships have been placed on the background.
        /// </summary>
        public void CreateAndPlaceEnemyShips()
        {
            var count = 4;
            this.getEnemyShips();
            for (var index = 0; index < this.EnemyShips.Count; index++)
            {
                this.gameBackground.Children.Add(this.EnemyShips[index].Sprite);
                if (index < (int)EnemyAmounts.Level1EnemyCount)
                {
                    this.placeEnemyShips(count - 1, this.EnemyShips[index]);
                }

                else if (index < (int)EnemyAmounts.Level2EnemyCount)
                {
                    this.placeEnemyShips(count - 4, this.EnemyShips[index]);
                }

                else if (index < (int)EnemyAmounts.Level3EnemyCount)
                {
                    this.placeEnemyShips(count - 9, this.EnemyShips[index]);
                }
                else
                {
                    this.placeEnemyShips(count - 16, this.EnemyShips[index]);
                }

                count++;

            }
            //this.placeBonusShip();
        }

        private void getEnemyShips()
        {
            this.EnemyShips = new List<EnemyShip>();

            for (var ship = 0; ship < 20; ship++)
            {
                if (ship < (int) EnemyAmounts.Level1EnemyCount)
                {
                    this.EnemyShips.Add(new EnemyShip(EnemyShipLevels.LevelOne));
                }
                else if (ship < (int) EnemyAmounts.Level2EnemyCount)
                {
                    this.EnemyShips.Add(new EnemyShip(EnemyShipLevels.LevelTwo));
                }
                else if (ship < (int) EnemyAmounts.Level3EnemyCount)
                {
                    this.EnemyShips.Add(new EnemyShip(EnemyShipLevels.LevelThree));
                }
                else
                {
                    this.EnemyShips.Add(new EnemyShip(EnemyShipLevels.LevelFour));
                }

            }
        }

        private void placeEnemyShips(int count, GameObject ship)
        {
            var offset = (this.gameBackground.Width - this.enemyShipsPerRow * ship.Width) / (this.enemyShipsPerRow + 1);
            ship.X = offset * (count + 1) + count * ship.Width;
            if (ship.Sprite is Level1EnemySprite)
            {
                ship.Y = 224;
            }
            else if (ship.Sprite is Level2EnemySprite)
            {
                ship.Y = 166;
            }
            else if (ship.Sprite is Level3EnemySprite)
            {
                ship.Y = 108;
            }
            else if (ship.Sprite is Level4EnemySprite)
            {
                ship.Y = 50;
            }
        }

        /// <summary>
        /// Places the bonus ship.
        /// </summary>
        public void PlaceBonusShip()
        {
            var random = new Random();
            random.Next(0, 100);
            if (!this.containsBonusShip())
            {
                if (random.Next(0, 100) > 98)
                {
                    this.EnemyShips.Add(new EnemyShip(EnemyShipLevels.Bonus));
                    this.gameBackground.Children.Add(this.EnemyShips[this.EnemyShips.Count - 1].Sprite);
                    this.EnemyShips[this.EnemyShips.Count - 1].X = 25;
                    this.EnemyShips[this.EnemyShips.Count - 1].Y = 5;
                    SoundPlayer.PlaySound("bonusShip.wav");
                }
            }
            else if (this.EnemyShips[this.EnemyShips.Count - 1].X + this.EnemyShips[this.EnemyShips.Count - 1].SpeedX + this.EnemyShips[this.EnemyShips.Count - 1].Width< this.gameBackground.Width)
            {
                this.EnemyShips[this.EnemyShips.Count -1].MoveRight();
            }
            else
            {
                this.gameBackground.Children.Remove(this.EnemyShips[this.EnemyShips.Count - 1].Sprite);
                this.EnemyShips.Remove(this.EnemyShips[this.EnemyShips.Count - 1]);
            }
        }

        private bool containsBonusShip()
        {
            foreach (var ship in this.EnemyShips)
            {
                if (ship.Sprite is BonusEnemySprite)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the firing enemies.
        /// </summary>
        /// <returns>the list of enemies that fire.</returns>
        public IList<GameObject> GetFiringEnemies()
        {
            IList<GameObject> firingEnemies = new List<GameObject>();
            foreach (var ship in this.EnemyShips)
            {
                if (ship.Sprite is Level3EnemySprite || ship.Sprite is Level4EnemySprite)
                {
                    firingEnemies.Add(ship);
                }
            }

            return firingEnemies;
        }

        /*private void placeBulletsBellowEnemies(GameObject ship, GameObject bullet)
        {
            bullet.X = ship.X;
            bullet.Y = ship.Y + 18;
        }*/

        /// <summary>
        /// Checks if an enemy ship is hit by a bullet
        /// Precondition: none
        /// Post-condition: the enemy and bullet should be removed if the ship is hit
        /// </summary>
        public IDictionary<GameObject, EnemyShip> EnemyDestroyed(IList<GameObject> playerBullets)
        {
            IDictionary<GameObject, EnemyShip> result = new Dictionary<GameObject, EnemyShip>();
            EnemyShip destroyedShip = null;
            GameObject hitBullet = null;
            foreach (var ship in this.EnemyShips)
            {
                destroyedShip = this.shipDestroyed(ship, destroyedShip, ref hitBullet, playerBullets);
            }

            this.EnemyShips.Remove(destroyedShip);
            
            if (hitBullet != null)
            {
                result.Add(hitBullet, destroyedShip);
            }

            return result;
        }

        private EnemyShip shipDestroyed(EnemyShip ship, EnemyShip destroyedShip, ref GameObject hitBullet, IList<GameObject> playerBullets)
        {
            foreach (var bullet in playerBullets)
            {
                if (CollisionDetector.DetectCollision(ship, bullet))
                {
                    this.gameBackground.Children.Remove(ship.Sprite);
                    if (bullet.Sprite is PowerUpSprite && ((PowerUp)bullet).HitCount == 0)
                    {
                        this.gameBackground.Children.Remove(bullet.Sprite);
                        hitBullet = bullet;
                    }

                    if (bullet.Sprite is PowerUpSprite && ((PowerUp)bullet).HitCount != 0)
                    {
                        ((PowerUp)bullet).HitCount -= 1;
                    }
                    else
                    {
                        this.gameBackground.Children.Remove(bullet.Sprite);
                        hitBullet = bullet;
                    }

                    SoundPlayer.PlaySound("explosion.wav");
                    destroyedShip = ship;
                    this.BulletFired = false;
                }
            }

            return destroyedShip;
        }

        /// <summary>
        /// Moves the enemy ships to the left.
        /// Precondition: none
        /// Post-condition: The enemy ships have moved left.
        /// </summary>
        public void MoveEnemyShipsLeft()
        {
            foreach (var ship in this.EnemyShips)
            {
                if (this.EnemyShips.Count > 0)
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
            foreach (var ship in this.EnemyShips)
            {
                if (this.EnemyShips.Count > 0)
                {
                    ship.MoveRight();
                }
            }
        }

        /// <summary>
        /// Moves the enemy snips level2.
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        public void MoveEnemySnipsLevel2()
        {
            foreach (var currentShip in this.EnemyShips)
            {
                if (this.EnemyShips.Count > 0)
                {
                    if (currentShip.Sprite is Level1EnemySprite || currentShip.Sprite is Level3EnemySprite)
                    {
                        if (this.level2FirstStep)
                        {
                            currentShip.MoveRight();
                        }
                        else if (this.level2StepCounter <= 3)
                        {
                            currentShip.MoveLeft();
                        }
                        else if (this.level2StepCounter <= 7)
                        {
                            currentShip.MoveRight();
                        }

                    }
                    else if (currentShip.Sprite is Level2EnemySprite || currentShip.Sprite is Level4EnemySprite)
                    {
                        if (this.level2FirstStep)
                        {
                            currentShip.MoveLeft();
                        }
                        else if (this.level2StepCounter <= 3)
                        {
                            currentShip.MoveRight();
                        }
                        else if (this.level2StepCounter <= 7)
                        {
                            currentShip.MoveLeft();
                        }

                    }
                }
            }
            if (this.level2StepCounter == 7)
            {
                this.level2StepCounter = 0;
            }
            else if (!this.level2FirstStep)
            {
                this.level2StepCounter++;
            }

            if (this.level2FirstStep && this.level2FirstStepCounter == 1)
            {
                this.level2FirstStep = false;
            }
            else if (this.level2FirstStep && this.level2FirstStepCounter == 0)
            {
                this.level2FirstStepCounter++;
            }

        }

        /// <summary>
        /// Moves the enemy ships level3.
        /// Precondition: none
        /// Post-condition: none
        /// </summary>
        public void MoveEnemyShipsLevel3()
        {
            foreach (var currentShip in this.EnemyShips)
            {
                if (this.EnemyShips.Count > 0)
                {
                    if (this.level3FirstStep)
                    {
                        currentShip.MoveRight();
                    }

                    if (this.level3StepCounter < 1)
                    {
                        //currentShip.MoveDown();
                        currentShip.Y = currentShip.Y + 10;
                    }
                    else if (this.level3StepCounter < 5)
                    {
                        currentShip.MoveLeft();
                    }
                    else if (this.level3StepCounter < 6)
                    {
                        //currentShip.MoveUp();
                        currentShip.Y = currentShip.Y - 10;
                    }
                    else if (this.level3StepCounter < 10)
                    {
                        currentShip.MoveRight();
                    }
                }
            }

            if (this.level3StepCounter == 9)
            {
                this.level3StepCounter = 0;
            }
            else
            {
                this.level3StepCounter++;
            }

            if (this.level3FirstStep && this.level3FirstStepCounter == 1)
            {
                this.level3FirstStep = false;
            }
            else if (this.level3FirstStep && this.level3FirstStepCounter == 0)
            {
                this.level3FirstStepCounter++;
            }

        }

        #endregion

    }
}
