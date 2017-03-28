Imports System.IO
Public Class Customers

    'Creating a structure with name, lastname and email. To be used when storing data in a text file

    Private Structure CustomerData
        Dim FirstName As String
        Dim LastName As String
        Dim Email As String
        Dim Address As String
        Dim Phone As String
        Dim DOB As String
        Dim SessionID As Integer
    End Structure


    Private Sub btnInput_Click(sender As System.Object, e As System.EventArgs) Handles btnInput.Click

        'Above i am declaring and giving value to the persons details

        Dim FirstName As String = txtFirstName.Text
        Dim LastName As String = txtLastName.Text
        Dim Email As String = txtEmail.Text
        Dim Address As String = txtAddress.Text
        Dim Phone As String = txtPhone.Text
        Dim DOB As String = dtpDoB.Text
        Dim SessionID As Integer
        SessionID = Val(txtSession.Text)


        ' This is simple validation using a function which is below, it checks if all the variables are not null or are too big 
        ' The first value is the actual data and the second number is how long the string is allowed to be
        If LengthCheck(Name, 35) = False Or LengthCheck(LastName, 35) = False Or LengthCheck(Email, 255) = False Or LengthCheck(Address, 100) = False Or LengthCheck(Phone, 11) = False Or LengthCheck(SessionID, 4) = False Then
            MsgBox(" Please Fill out all the fields.")
        ElseIf DataTypeCheck(FirstName, False) = False Or DataTypeCheck(LastName, False) = False Or DataTypeCheck(Phone, True) = False Or DataTypeCheck(SessionID, True) = False Then
            MsgBox("Please use the correct data types")

        Else


            Dim SessionCopy As Boolean = False
            Dim Check() As String = File.ReadAllLines(Dir$("Customer.txt"))
            For I = 0 To UBound(Check)

                If Trim(Mid(Check(I), 1, 4)) = txtSession.Text Then SessionCopy = True


            Next I

            If SessionCopy = False Then


                'If it passes the validation i want to write it to a text box which is what happens here.

                Dim CustomerDetails As New CustomerData

                Dim sw As New StreamWriter(Application.StartupPath & "\Customer.txt", True)
                CustomerDetails.FirstName = LSet(txtFirstName.Text, 35)
                CustomerDetails.LastName = LSet(txtLastName.Text, 35)
                CustomerDetails.Email = LSet(txtEmail.Text, 255)                'Filling the structure with data.
                CustomerDetails.Address = LSet(txtAddress.Text, 100)
                CustomerDetails.Phone = LSet(txtPhone.Text, 11)
                CustomerDetails.DOB = LSet(dtpDoB.Text, 20)
                CustomerDetails.SessionID = LSet(Val(txtSession.Text), 4)
                sw.WriteLine(CustomerDetails.SessionID & CustomerDetails.LastName & CustomerDetails.Email & CustomerDetails.Address & CustomerDetails.Phone & CustomerDetails.DOB & CustomerDetails.FirstName)
                sw.Close()

                MsgBox("Saved")

            Else : MsgBox("Please choose a new session ID that one is taken")

            End If


        End If


    End Sub

    Private Function LengthCheck(ByVal Data As String, Length As Integer)

        'This function has two inputs, the actual data and the length the data is allowed to be 
        'If the length is too big or the data is null then False is returned otherwise True is returned
        If Data.Length > Length Then
            Return False

        ElseIf Data = "" Then
            MsgBox("please ensure all fields are full")
            Return False
        Else
            Return True
        End If


    End Function

    Private Function DataTypeCheck(ByVal Data As String, Type As Boolean)

        'This function has two inputs, the actual data and the type of this dats in the form of boolean
        'If the boolean is true then the data contains numbers and no letters
        'If the value is false then it contains letters and no numbers
        'isnumeric is used to check whether numebers are used and the corresponding true/false value returned

        If Type = True Then
            If IsNumeric(Data) = True Then
                Return True

            Else : Return False

            End If



        ElseIf Type = False Then

            If IsNumeric(Data) = False Then

                Return True

            Else : Return False

            End If

        End If

        Return False

    End Function

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click

        'Here i am declaring and assigning the value of the search they want to search
        Dim SearchValue As String = txtInputSearch.Text

        If LengthCheck(SearchValue, 4) = False Or DataTypeCheck(SearchValue, True) = False Then
            MsgBox("Please ensure the search value is a 4 digit Session ID")
            'Above checks whether their search value fits what i allow using my functions
        Else
            Dim Found As Boolean
            Found = False
            'Found will be an identifyer 
            'This is assigning the value of the text document to CustomerData
            Dim Customerdata() As String = File.ReadAllLines(Dir$("Customer.txt"))
            For I = 0 To UBound(Customerdata)
                'This For loop will repeat for all the lines in the text file
                If Trim(Mid(Customerdata(I), 1, 4)) = txtInputSearch.Text And Not txtInputSearch.Text = "" Then
                    Found = True
                End If

                'The above if statement will find if this line in the text document contains the search value

                If Found = True Then


                    txtSession.Text = Trim(Mid(Customerdata(I), 1, 4))
                    txtLastName.Text = Trim(Mid(Customerdata(I), 5, 35))
                    txtEmail.Text = Trim(Mid(Customerdata(I), 40, 255))
                    txtAddress.Text = Trim(Mid(Customerdata(I), 295, 100))
                    txtPhone.Text = Trim(Mid(Customerdata(I), 395, 11))
                    dtpDoB.Text = Trim(Mid(Customerdata(I), 406, 20)) & " 11:00AM"

                    txtFirstName.Text = Trim(Mid(Customerdata(I), 426, 35))
                    'These are all statements that assign the value in the text document line to the textboxes
                    MsgBox("SearchFound")
                    I = UBound(Customerdata)
                    'Above stops the for loops
                End If

            Next I

            If Found = False Then
                MsgBox("Search not found please ensure the value is correct")
            End If
        End If

    End Sub
End Class