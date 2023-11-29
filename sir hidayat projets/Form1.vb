Imports System.IO
Public Class Form1
    ' Variable to store the current file path
    Private currentFilePath As String = ""
    ' Variable to store the current zoom factor
    Private currentZoomFactor As Single = 1.0


    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub CompileAndRunToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompileAndRunToolStripMenuItem.Click
        Try
            Dim process As New Process()
            process.StartInfo.FileName = "python.exe"
            process.StartInfo.Arguments = $"-c ""{RichTextBox1.Text}"""
            process.StartInfo.UseShellExecute = False
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.CreateNoWindow = True
            process.Start()

            ' Read the output and error streams
            Dim output As String = process.StandardOutput.ReadToEnd()
            Dim errors As String = process.StandardError.ReadToEnd()

            ' Display the output and errors in a MessageBox
            If String.IsNullOrEmpty(errors) Then
                MessageBox.Show(output, "Output")
            Else
                MessageBox.Show(errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

            process.WaitForExit()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub CopyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        If RichTextBox1.SelectionLength > 0 Then
            RichTextBox1.Copy()
        End If
    End Sub

    Private Sub CutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CutToolStripMenuItem.Click
        If RichTextBox1.SelectionLength > 0 Then
            RichTextBox1.Cut()
        End If
    End Sub



    Private Sub PasteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        RichTextBox1.Paste()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFile()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        SaveFileAs()
    End Sub
    Private Sub SaveFile()
        If String.IsNullOrEmpty(currentFilePath) Then
            ' If the file has not been saved yet, use SaveFileAs
            SaveFileAs()
        Else
            ' Save the file using the current file path
            File.WriteAllText(currentFilePath, RichTextBox1.Text)
        End If
    End Sub

    Private Sub SaveFileAs()
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "Text Files|*.txt|All Files|*.*"

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            ' Save the file using the chosen file path
            File.WriteAllText(saveFileDialog.FileName, RichTextBox1.Text)
            currentFilePath = saveFileDialog.FileName
        End If
    End Sub

    Private Sub ZoomInToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomInToolStripMenuItem.Click
        ZoomIn()
    End Sub

    Private Sub ZoomOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomOutToolStripMenuItem.Click
        Zoomout()
    End Sub
    Private Sub ZoomIn()
        ' Increase the zoom factor by 0.1 (adjust as needed)
        currentZoomFactor += 0.1
        ApplyZoom()
    End Sub

    Private Sub ZoomOut()
        ' Decrease the zoom factor by 0.1 (adjust as needed)
        currentZoomFactor -= 0.1
        ApplyZoom()
    End Sub

    Private Sub ApplyZoom()
        ' Set the ZoomFactor property of the RichTextBox
        RichTextBox1.ZoomFactor = currentZoomFactor
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        RichTextBox1.Text = ""
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If PromptToSaveChanges() Then
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Text Files|*.txt|All Files|*.*"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                RichTextBox1.Text = File.ReadAllText(openFileDialog.FileName)
                currentFilePath = openFileDialog.FileName
            End If
        End If
    End Sub

    ' ...

    Private Function PromptToSaveChanges() As Boolean
        If RichTextBox1.Modified Then
            Dim result As DialogResult = MessageBox.Show("Do you want to save changes?", "Save Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                SaveFile() ' Save the changes
                Return True
            ElseIf result = DialogResult.No Then
                Return True ' Continue without saving
            Else
                Return False ' Cancel the operation
            End If
        End If

        Return True
    End Function

    Private Sub AboutUsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutUsToolStripMenuItem.Click
        MessageBox.Show("Ahsan Raza, the group leader, along with team members Muhammad Shafiullah, Zoha Batool, and Urooj Fatima, are working on a collaborative project for their BS-CS III. The project involves designing and implementing a basic compiler, But They Created a Python IDE Instead. The goal is to create a functional compiler for a simple programming language.", "About US", MessageBoxButtons.OK, MessageBoxIcon.None)
    End Sub
End Class




