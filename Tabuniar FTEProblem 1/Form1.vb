Public Class Form1

    ' Define Artwork class
    Public Class Artwork
        Public Property Title As String
        Public Property Artist As String
        Public Property Category As String
        Public Property Price As Decimal
    End Class

    ' Collection to store artworks
    Private artworks As New List(Of Artwork)

    ' Add Artwork Button
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            ' Validate inputs
            If txtTitle.Text.Trim() = "" Or txtArtist.Text.Trim() = "" Or cboCategory.Text.Trim() = "" Or txtPrice.Text.Trim() = "" Then
                MessageBox.Show("Please complete all fields!", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' Validate numeric price
            Dim price As Decimal
            If Not Decimal.TryParse(txtPrice.Text, price) Then
                MessageBox.Show("Please enter a valid numeric price.", "Invalid Price", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            ' Create new artwork
            Dim newArt As New Artwork With {
                .Title = txtTitle.Text.Trim(),
                .Artist = txtArtist.Text.Trim(),
                .Category = cboCategory.Text.Trim(),
                .Price = price
            }

            ' Add to list
            artworks.Add(newArt)

            ' Refresh DataGridView
            dgvArtworks.DataSource = Nothing
            dgvArtworks.DataSource = artworks

            MessageBox.Show("Artwork added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Optional: Clear inputs
            txtTitle.Clear()
            txtArtist.Clear()
            txtPrice.Clear()
            cboCategory.SelectedIndex = -1

        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Analyze Artwork Button
    Private Sub btnAnalyze_Click(sender As Object, e As EventArgs) Handles btnAnalyze.Click
        If artworks.Count = 0 Then
            MessageBox.Show("No artworks to analyze.", "Empty List", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' LINQ computations
        Dim totalArtworks = artworks.Count
        Dim avgPrice = artworks.Average(Function(a) a.Price)
        Dim highest = artworks.OrderByDescending(Function(a) a.Price).First()
        Dim lowest = artworks.OrderBy(Function(a) a.Price).First()

        ' Category breakdown
        Dim categoryCounts = artworks.
            GroupBy(Function(a) a.Category).
            Select(Function(g) $"{g.Key}: {g.Count()}").
            ToList()

        ' Display results
        txtResults.Text =
            $"Total Artworks: {totalArtworks}" & vbCrLf &
            $"Average Price: ₱{avgPrice:N2}" & vbCrLf &
            $"Highest Priced Artwork: '{highest.Title}' by {highest.Artist} (₱{highest.Price:N2})" & vbCrLf &
            $"Lowest Priced Artwork: '{lowest.Title}' by {lowest.Artist} (₱{lowest.Price:N2})" & vbCrLf &
            "Category Breakdown:" & vbCrLf & String.Join(vbCrLf, categoryCounts)
    End Sub

    ' Clear Button
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        artworks.Clear()
        dgvArtworks.DataSource = Nothing
        txtResults.Clear()
        txtTitle.Clear()
        txtArtist.Clear()
        txtPrice.Clear()
        cboCategory.SelectedIndex = -1
        MessageBox.Show("All records cleared.", "Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class
