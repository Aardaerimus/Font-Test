Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Namespace FontTest
    Public Class Game1
        Inherits Game
        Private _graphics As GraphicsDeviceManager
        Private _spriteBatch As SpriteBatch
        Private _gameCore As GameCore

        ' Background Color
        Private _bgColor As New Color(20, 20, 20, 255)


        Public Sub New()
            _graphics = New GraphicsDeviceManager(Me)
            Content.RootDirectory = "Content"
            IsMouseVisible = True
        End Sub

        Protected Overrides Sub Initialize()
            ' TEST - SET RESOLUTION
            _graphics.PreferredBackBufferWidth = 1152 '1280 '1152
            _graphics.PreferredBackBufferHeight = 756 '680

            _graphics.ApplyChanges()

            MyBase.Initialize()
        End Sub

        Protected Overrides Sub LoadContent()
            _spriteBatch = New SpriteBatch(GraphicsDevice)

            ' SET GLOBALS
            Globals.SpriteBatch = _spriteBatch
            Globals.Graphics = _graphics
            Globals.Content = Content
            Globals.Screen = New Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight)
            Globals.Random = New Random

            ' LOAD CONTENT
            Textures.Load()
            Fonts.Load()
            Sounds.Load()

            ' LOAD GAME
            _gameCore = New GameCore
            _gameCore.Load()
        End Sub

        Public Sub ToggleFullScreen()
            Globals.Graphics.ToggleFullScreen()
            Globals.Graphics.ApplyChanges()
        End Sub

        Protected Overrides Sub Update(gameTime As GameTime)
            Globals.Update(gameTime)
            Input.Update()

            ' CLOSE GAME
            If Input.KeyPressed(Keys.Escape) = True Then [Exit]()
            If Input.KeyPressed(Keys.F1) Then ToggleFullScreen()

            _gameCore.Update()

            MyBase.Update(gameTime)
        End Sub

        Protected Overrides Sub Draw(gameTime As GameTime)
            'GraphicsDevice.Clear(Color.White)
            'GraphicsDevice.Clear(Color.Black)
            GraphicsDevice.Clear(_bgColor)

            _gameCore.Draw()

            'Globals.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, Nothing, transformMatrix:=_camera)
            'Globals.SpriteBatch.Begin()
            'Globals.SpriteBatch.End()

            MyBase.Draw(gameTime)
        End Sub
    End Class
End Namespace
