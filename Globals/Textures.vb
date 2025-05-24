Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Content

Public Class Textures
    Public Shared Menu As Texture2D
    Public Shared AdvDialogIcon As Texture2D ' REQ: Dialog Class
    Public Shared DialogBackground As Texture2D ' REQ: Dialog Class

    Public Shared Sub Load()
        Menu = Globals.Content.Load(Of Texture2D)("GFX/menu1")
        AdvDialogIcon = Globals.Content.Load(Of Texture2D)("GFX/AdvDialogiIcon")
        DialogBackground = Globals.Content.Load(Of Texture2D)("GFX/DialogBackground")
    End Sub
End Class
