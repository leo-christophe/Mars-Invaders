﻿using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using SAE101;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsInvader
{
    internal class Alien
    {
        private int health;
        private AnimatedSprite alienTexture;
        private Vector2 positionAlien;
        private int attack;
        private int speed;
        private TiledMap tiledMap;
        private int niveau;
        public Rectangle hitBox;
        private float _attackCooldown;
        private int nbAliensSpawnN1;
        private int nbAliensSpawnN2;
        private int nbAliensSpawnN3;
        private int nbAliensSpawnN4;
        private bool _touchedPlayer;
        private SpriteSheet spriteSheetN4;
        public const int ALIENSIZE = 24;
        public const int MAXALIENHEALTH = 50;
        public const double PADDING = 0.9;
        public const float MAXATTACKCOOLDOWN = 80;
        public const int ALIENATTACKBASE = 10;

        public Alien( int NiveauA,TiledMap _tiledMap/*, SpriteSheet spriteSheetN1, SpriteSheet spriteSheetN2, SpriteSheet spriteSheetN3*/, SpriteSheet spriteSheetN4)
        {
            Random rnd = new Random();
            nbAliensSpawnN1=1;
            nbAliensSpawnN2=0;
            nbAliensSpawnN3=0;
            nbAliensSpawnN4=0;
            this.Health = Alien.MAXALIENHEALTH*NiveauA;
            this.Niveau = NiveauA;
            this.Attack = ALIENATTACKBASE* NiveauA;
            this.spriteSheetN4 = spriteSheetN4;
            this.Speed = 100;
            this.TiledMap = _tiledMap;
            /*if(this.Niveau==1)*/ 
            this.AlienTexture = new AnimatedSprite(spriteSheetN4);
            /*else if (this.Niveau == 2) this.AlienTexture = new AnimatedSprite(spriteSheetN2);
            else if (this.Niveau == 3) this.AlienTexture = new AnimatedSprite(spriteSheetN3);
            else if (this.Niveau == 4) this.AlienTexture = new AnimatedSprite(spriteSheetN4);*/


            this.AttackCooldown = Alien.MAXATTACKCOOLDOWN*3;
            this.TouchedPlayer = true;


            this.PositionAlien = new Vector2(rnd.Next(50, Game1._WINDOWSIZE - 50), rnd.Next(50, Game1._WINDOWSIZE - 50)); 
            this.hitBox = new Rectangle((int)this.PositionAlien.X, (int)this.PositionAlien.Y, (int)(ALIENSIZE * PADDING), (int)(ALIENSIZE * PADDING));
        }

        public int Health
        {
            get
            {
                return this.health;
            }

            set
            {
                if (value <= Alien.MAXALIENHEALTH && !String.IsNullOrEmpty(value.ToString()))
                    this.health = value;
                else
                    throw new ArgumentOutOfRangeException("La vie a une erreur de valeur, est soit inférieure ou égale à 0 ou supérieure à 100 ou vide / null.");
            }
        }

        public AnimatedSprite AlienTexture
        {
            get
            {
                return this.alienTexture;
            }

            set
            {
                this.alienTexture = value;
            }
        }

        public Vector2 PositionAlien
        {
            get
            {
                return this.positionAlien;
            }

            set
            {
                this.positionAlien = value;
                this.hitBox = new Rectangle((int)value.X, (int)value.Y, 24, 24);
            }
        }

        public int Attack
        {
            get
            {
                return this.attack;
            }

            set
            {
                if (value > 0 && !String.IsNullOrEmpty(value.ToString()))
                    this.attack = value;
                else
                    throw new ArgumentOutOfRangeException("L'attaque a une erreur de valeur, est soit inférieure ou égale à 0, soit vide.");
            }
        }

        public int Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                if (value >= 0 && !String.IsNullOrEmpty(value.ToString()))
                    this.speed = value;
                else
                    throw new ArgumentNullException("La vitesse a une erreur de valeur, soit inférieure ou égale à 0, soit vide.");
            }
        }

        public TiledMap TiledMap
        {
            get
            {
                return this.tiledMap;
            }

            set
            {
                this.tiledMap = value;
            }
        }

        public int Niveau
        {
            get
            {
                return this.niveau;
            }

            set
            {
                if (value > 0 &&value<5 && !String.IsNullOrEmpty(value.ToString()))
                    this.niveau = value;
                else
                    throw new ArgumentNullException("La vitesse a une erreur de valeur, soit inférieure ou égale à 0, soit vide.");
            }
        }

        public float AttackCooldown
        {
            get
            {
                return this._attackCooldown;
            }

            set
            {
                if (!String.IsNullOrEmpty(value.ToString()) && value >= 0)
                    this._attackCooldown = value;
                else
                    throw new Exception("La période d'invincibilité du joueur a un problème, est soit vide, soit inférieure strictement à 0");
            }
        }

        public bool TouchedPlayer
        {
            get
            {
                return this._touchedPlayer;
            }

            set
            {
                this._touchedPlayer = value;
            }
        }
        public int nbAliensSpawn(int niveau, List<Alien> Aliens)
        {
            int aliensA=0;
            for (int i=0;i<Aliens.Count;i++)
            {
                if (Aliens[i].Niveau == niveau)
                {
                    aliensA++;
                }
                Console.WriteLine(Aliens[i].niveau);

            }
            int aliens = 0;
            if (niveau == 1) aliens = nbAliensSpawnN1;
            else if (niveau == 2) aliens = nbAliensSpawnN2;
            else if (niveau == 3) aliens = nbAliensSpawnN3;
            else  aliens = nbAliensSpawnN4;

            int diff = 0;
            if (aliensA < aliens)
            {
                diff = aliens - aliensA;
            }
            Console.WriteLine(diff);

            return diff;
        }

        public void directionOppAlien(GameTime gameTime, Vector2 Position )
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            alienTexture.Update(deltaTime);

            float walkSpeed = deltaTime * this.Speed; // Vitesse de déplacement du sprite

            if (this.PositionAlien.X - Position.X >= 0)//alien est a droite
            {
                if (this.PositionAlien.Y - Position.Y >= 0)//alien est en dessous a droite
                {
                    if (this.PositionAlien.X- Position.X  > this.PositionAlien.Y- Position.Y   )//X est plus pres
                    {
                        this.positionAlien.X += walkSpeed;
                    }
                    else
                    {
                        this.positionAlien.Y += walkSpeed;
                    }
                }
                else//alien est au dessus a droite
                {
                    if (this.PositionAlien.X - Position.X >  Position.Y - this.PositionAlien.Y)//X est plus pres
                    {
                        this.positionAlien.X += walkSpeed;
                    }
                    else
                    {
                        this.positionAlien.Y -= walkSpeed;
                    }
                }
            }
            else//alien est a gauche
            {
                if (this.PositionAlien.Y - Position.Y >= 0)//alien est en dessous a droite
                {
                    if  ( Position.X- this.PositionAlien.X  >  this.PositionAlien.Y - Position.Y)//X est plus pres
                    {
                        this.positionAlien.X -= walkSpeed;
                    }
                    else
                    {
                        this.positionAlien.Y += walkSpeed;
                    }
                }
                else//alien est au dessus a droite
                {
                    if (Position.X - this.PositionAlien.X > Position.Y - this.PositionAlien.Y )//X est plus pres
                    {
                        this.positionAlien.X -= walkSpeed;
                    }
                    else
                    {
                        this.positionAlien.Y += walkSpeed;
                    }
                }
                
            }

        }
        public void directionAlien(GameTime gameTime, Vector2 Position)
        {
            String animation = "idle";

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            alienTexture.Update(deltaTime);

            float walkSpeed = deltaTime * this.Speed; // Vitesse de déplacement du sprite

            if (this.PositionAlien.X - Position.X >= 0)//alien est a droite
            {
                if (this.PositionAlien.Y - Position.Y >= 0)//alien est en dessous a droite
                {
                    if (this.PositionAlien.X - Position.X > this.PositionAlien.Y - Position.Y)//X est plus pres
                    {
                        this.positionAlien.X -= walkSpeed;
                        animation = "walkWest";
                    }
                    else
                    {
                        this.positionAlien.Y -= walkSpeed;
                        animation = "walkNorth";
                    }
                }
                else//alien est au dessus a droite
                {
                    if (this.PositionAlien.X - Position.X > Position.Y - this.PositionAlien.Y)//X est plus pres
                    {
                        this.positionAlien.X -= walkSpeed;
                        animation = "walkWest";
                    }
                    else
                    {
                        this.positionAlien.Y += walkSpeed;
                        animation = "walkSouth";
                    }
                }
            }
            else//alien est a gauche
            {
                if (this.PositionAlien.Y - Position.Y >= 0)//alien est en dessous a droite
                {
                    if (Position.X - this.PositionAlien.X > this.PositionAlien.Y - Position.Y)//X est plus pres
                    {
                        this.positionAlien.X += walkSpeed;
                        animation = "walkEast";
                    }
                    else
                    {
                        this.positionAlien.Y -= walkSpeed;
                        animation = "walkNorth";
                    }
                }
                else//alien est au dessus a droite
                {
                    if (Position.X - this.PositionAlien.X > Position.Y - this.PositionAlien.Y)//X est plus pres
                    {
                        this.positionAlien.X += walkSpeed;
                        animation = "walkEast";
                    }
                    else
                    {
                        this.positionAlien.Y += walkSpeed;
                        animation = "walkSouth";
                    }
                }

            }

            // UPDATE ALIEN
            alienTexture.Play(animation);
        }

        public void alienAttackCooldown()
        /// Cette méthode permet au joueur d'être "invincible" lorsque le joueur est touché, pendant une petite période
        {
            if (this.AttackCooldown <= 0)
            {
                this.AttackCooldown = MAXATTACKCOOLDOWN;
                this.TouchedPlayer = false;
            }
            else if (this.TouchedPlayer == true)
            {
                this.AttackCooldown -= 1;
            }
        }

        public void updateAlien(GameTime gameTime, Vector2 positionJoueur)
        /// Cette méthode gère strictement les aliens à chaque rafraichissement
        {
            this.directionAlien(gameTime, positionJoueur);
            this.alienAttackCooldown();
            this.hitBox = new Rectangle((int)this.PositionAlien.X, (int)this.PositionAlien.Y, 20, 20);
        }
    }


    
}
