Imports System

Module Program
    Public Sub Main()
        Dim result As String
        Dim hexString As String

        hexString = "0x39"
        Console.WriteLine("Test hex: " & hexString)
        result = Execute(hexString)
        Console.WriteLine("Expecting 11001, output " & result)
        Console.WriteLine("")

        hexString = "0x3F"
        Console.WriteLine("Test hex: " & hexString)
        result = Execute(hexString)
        Console.WriteLine("Expecting 11111, output " & result)
        Console.WriteLine("")

        hexString = "0x3E"
        Console.WriteLine("Test hex: " & hexString)
        result = Execute(hexString)
        Console.WriteLine("Expecting  11110, output " & result)
        Console.WriteLine("")
    End Sub
    Public Function Execute(ByVal hexString As String) As String
        Dim Number As UInt16 = Convert.ToUInt16(hexString, 16)
        'Console.WriteLine("With most significant bit: " & Convert.ToString(Number, 2))

        'find most significant bit
        Dim MSB = HowManyTimesToShiftRight(Number) - 1 ' why minus 1 ah
        'Console.WriteLine("Most significant bit: " & MSB)

        Dim BitToClear As SByte = 1 << MSB
        Dim WithoutMSB = Number And Not BitToClear

        Return Convert.ToString(WithoutMSB, 2).TrimStart("0")
        'console.WriteLine("Without most significant bit: " & Convert.ToString(WithoutMSB, 2))
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
