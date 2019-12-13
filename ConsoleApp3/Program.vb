Imports System

Module Program
    Public Sub Main()
        Dim result As String
        Dim hexString As String

        hexString = "0xFF"
        Console.WriteLine("Test hex: " & hexString)
        result = Execute(hexString)
        Console.WriteLine("Expecting [1111111], output " & result)
        Console.WriteLine("")

        hexString = "0x77"
        Console.WriteLine("Test hex: " & hexString)
        result = Execute(hexString)
        Console.WriteLine("Expecting [110111], output " & result)
        Console.WriteLine("")

        hexString = "0x3F"
        Console.WriteLine("Test hex: " & hexString)
        result = Execute(hexString)
        Console.WriteLine("Expecting [11111], output " & result)
        Console.WriteLine("")

        hexString = "0x3E"
        Console.WriteLine("Test hex: " & hexString)
        result = Execute(hexString)
        Console.WriteLine("Expecting  [11110], output " & result)
        Console.WriteLine("")

        hexString = "0x400001"
        Console.WriteLine("Test hex: " & hexString)
        result = Execute(hexString)
        Console.WriteLine("Expecting  [1000000], output " & result)
        Console.WriteLine("")

        hexString = "0xFF773F3E"
        Console.WriteLine("Test hex: " & hexString)
        result = Execute(hexString)
        Console.WriteLine("Expecting  [11111111 1110111 111111 11110], output " & result)
        Console.WriteLine("")

    End Sub
    Public Function Execute(ByVal hexString As String) As String

        ' remove 0x
        If hexString.Substring(0, 2) = "0x" Then
            hexString = hexString.Substring(2)
        End If

        ' process hex in pairs
        Dim hexList = Enumerable.Range(0, hexString.Length \ 2).[Select](Function(i) hexString.Substring(i * 2, 2)).ToList()

        Dim count As Integer = 0
        Dim binaryWithoutMSB As String
        For Each hexItem In hexList
            count = count + 1
            If count = hexList.Count Then
                'find most significant bit on the rightmost hex and remove
                Dim Number As UInt32 = Convert.ToUInt32(hexItem, 16)
                Dim MSB = HowManyTimesToShiftRight(Number) - 1

                Dim BitToClear As UInt32 = 1 << MSB
                Dim WithoutMSB = Number And Not BitToClear
                binaryWithoutMSB = binaryWithoutMSB & " " & Convert.ToString(WithoutMSB, 2).TrimStart("0") ' trim off leading empty bits
            Else
                binaryWithoutMSB = binaryWithoutMSB & " " & Convert.ToString(Convert.ToUInt16(hexItem, 16), 2)
            End If
        Next

        ' once complete, remove leading (from behind, endian-ness) zero bytes - if the leading array is 00
        Dim binaryList = binaryWithoutMSB.Trim.Split(" ").ToList
        If binaryList(binaryList.Count - 1) = "0" Then
            binaryList.RemoveAt(binaryList.Count - 1)
        End If
        binaryWithoutMSB = String.Join(" ", binaryList)

        Return binaryWithoutMSB.Trim
    End Function

    Public Function HowManyTimesToShiftRight(ByVal Value As UShort) As Integer
        Dim count As Integer = 0

        While Value > 0
            count = count + 1
            Value = Value >> 1
        End While

        Return count
    End Function
End Module
