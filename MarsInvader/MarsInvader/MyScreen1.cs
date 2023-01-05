﻿using System;
using MarsInvader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using SAE101;
using MonoGame.Extended.Serialization;

public class MyScreen1 : GameScreen
	{
		private Game1 _myGame;
		public TiledMap _tiledMap;
		private TiledMapRenderer _tiledMapRenderer;
		private SpriteBatch _spriteBatch { get; set; }
		Player _joueur;
		Alien[] _alien=new Alien[10] ;
		private TiledMapTileLayer mapLayer;
		private Texture2D _cible;

	// pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est
	// défini dans Game1
	public MyScreen1(Game1 game) : base(game)
		{
			_myGame = game;
		}
    public override void Initialize()
    {
		base.Initialize();
    }
    public override void LoadContent()
		{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
		SpriteSheet spriteSheetAstro = Content.Load<SpriteSheet>("astroAnimation.sf", new JsonContentLoader());
		SpriteSheet spriteSheetAlien1 = Content.Load<SpriteSheet>("alienLV1.sf", new JsonContentLoader());
		SpriteSheet spriteSheetAlien2 = Content.Load<SpriteSheet>("alienLV2.sf", new JsonContentLoader());
		SpriteSheet spriteSheetAlien3 = Content.Load<SpriteSheet>("alienLV3.sf", new JsonContentLoader());
		SpriteSheet spriteSheetAlien4 = Content.Load<SpriteSheet>("alienLV4.sf", new JsonContentLoader());

		_cible = Content.Load<Texture2D>("cible");
		_tiledMap = Content.Load<TiledMap>("map_V1");
		_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
		mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
		_joueur  = new Player("Jed",_tiledMap, mapLayer, spriteSheetAstro);
		for (int i=0; i<10;i++)
        {
			_alien[i] = new Alien(1, _tiledMap, spriteSheetAlien3);
		}

		base.LoadContent();
	}
	public override void Update(GameTime gameTime)
	{
		_joueur.Deplacer(gameTime);
		_tiledMapRenderer.Update(gameTime);
	}
		public override void Draw(GameTime gameTime)
		{
		_tiledMapRenderer.Draw();
		_spriteBatch.Begin();
		_spriteBatch.Draw(_joueur.Perso, _joueur.PositionPerso);
		for (int i = 0; i < 10; i++)
		{
			_spriteBatch.Draw(_alien[i].AlienTexture, _alien[i].PositionAlien);
		}
		_spriteBatch.End();




		// on utilise la reference vers
		// Game1 pour chnager le graphisme
	}
	
}

