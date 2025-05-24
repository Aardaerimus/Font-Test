Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Content

Public Class Globals
    Public Shared State As GameState = GameState.Start

    ' CORE GAME COMPONENTS
    Public Shared Graphics As GraphicsDeviceManager
    Public Shared SpriteBatch As SpriteBatch
    Public Shared Camera As Matrix
    Public Shared Content As ContentManager
    Public Shared Random As Random
    Public Shared Screen As Rectangle
    Public Shared GameTimer As Double

    Public Shared Sub Update(GT As GameTime)
        GameTimer = GT.ElapsedGameTime.TotalMilliseconds
    End Sub

    Public Enum GameState
        None
        Start
        Running
        Paused
        GameOver
    End Enum
End Class
