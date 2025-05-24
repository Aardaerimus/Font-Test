Imports FontTest.FontTest

#If WINDOWS OrElse LINUX Then

Public Module Program
    <STAThread> Sub Main()
        Using game As New Game1()
            game.Run()
        End Using
    End Sub
End Module
#End If
