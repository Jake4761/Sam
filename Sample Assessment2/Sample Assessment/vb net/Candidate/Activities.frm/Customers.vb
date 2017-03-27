Imports System.IO
Public Class Customers

    'Creating a structure with name, lastname and email. To be used when storing data in a text file

    Private Structure CustomerData
        Dim Name As String
        Dim LastName As String
        Dim Email As String
    End Structure


    Private Sub btnInput_Click(sender As System.Object, e As System.EventArgs) Handles btnInput.Click
        Dim Name As String = txtName.Text
        Dim LastName As String = txtLastName.Text
        Dim Email As String = txtEmail.Text
        'Above i am declaring and giving value to the persons details


        ' This is simple validation using a function which is below, it checks if all the variables are not null or are too big 
        ' The first value is the actual data and the second number is how long the string is allowed to be
        If Validation(Name, 35) = False Or Validation(LastName, 35) = False Or Validation(Email, 255) = False Then
            MsgBox(" Please Fill out all the fields.")
        Else








            'If it passes the validation i want to write it to a text box which is what happens here.

            Dim CustomerDetails As New CustomerData

            Dim sw As New StreamWriter(Application.StartupPath & "\Customer.txt", True)
            CustomerDetails.Name = LSet(txtName.Text, 35)
            CustomerDetails.LastName = LSet(txtLastName.Text, 35)
            CustomerDetails.Email = LSet(txtEmail.Text, 255)                'Filling the structure with data.

            sw.WriteLine(CustomerDetails.Name & CustomerDetails.LastName & CustomerDetails.Email)
            sw.Close()




        End If


    End Sub

    Private Function Validation(ByVal Data As String, Length As Integer)
        If Data.Length > Length Then
            Return False

        ElseIf Data = "" Then
            MsgBox("please ensure all fields are full")
            Return False
        Else
            Return True
        End If
        MsgBox(Data)

    End Function

End Class