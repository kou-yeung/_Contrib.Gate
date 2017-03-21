REM  *****  BASIC  *****

' CSV�o�͐ݒ��ێ�����\����
Type ExportInfo
	Path As String
	Sheet As String
	Filter As Integer
	Header As Integer
End Type

Function CurrentPath()
	CurrentPath = ConvertFromUrl(StarDesktop.CurrentComponent.getURL()) + "./../"
End Function

Function GetSheetByName( SheetName As String )
	Dim Doc As Object
	Doc = StarDesktop.CurrentComponent
	GetSheetByName = Doc.Sheets.getByName(SheetName)
End Function

' �V�[�g���w�肵�ăJ�[�\�����擾����
Function GetCursor( Sheet As Object)
	Dim Range As Object
	Dim Cursor As Object

	Range = Sheet.getCellRangeByName("A1")
	Cursor = Sheet.createCursorByRange(Range)
	Cursor.gotoEndOfUsedArea(True)

	GetCursor = Cursor
End Function

' csv_out �ɋL�ڂ��ꂽ�o�͏��ꗗ���쐬����
Function GetExportInfo
	Dim Sheet As Object
	Dim Cursor As Object
	Dim nRow As Integer
	
	Sheet = GetSheetByName("csv_out")
	Cursor = GetCursor(Sheet)
	
	Dim Infos$(5 To Cursor.Rows.Count - 1) As ExportInfo

	For nRow = 5 To Cursor.Rows.Count - 1
		Dim Info As ExportInfo
		Info.Path = CurrentPath() + Sheet.getCellByPosition(0, nRow).String
		Info.Sheet = Sheet.getCellByPosition(1, nRow).String
		Info.Filter = Sheet.getCellByPosition(2, nRow).Value - 1
		Info.Header = Sheet.getCellByPosition(3, nRow).Value - 1
		Infos(nRow) = Info
	Next nRow
	
	GetExportInfo = Infos
End Function

' �o�͏��ɂ��CSV�������o��
Sub ExportByInfo( Info As ExportInfo)

	Dim FileName As String
	Dim Cursor As Object
	Dim nRow As Integer
	Dim nColumn As Integer
	Dim Filter As String
	Dim Sheet As Object
	Dim Stream As Object
	
	' �V�[�g�擾
	Sheet = GetSheetByName(Info.Sheet)
	Cursor = GetCursor(Sheet)
	
	' �t�@�C���I�[�v��
	Set Stream = CreateObject("ADODB.Stream")
	Stream.Charset = "UTF-8"	' �X�g���[���̕����R�[�h��UTF8�ɐݒ肷��
	Stream.Type = 2	' �t�@�C���̃^�C�v(1:�o�C�i�� 2:�e�L�X�g)
 	Stream.Open	' �X�g���[�����J��

	FileName = Info.Path + Info.Sheet + ".csv"

	' ���R�[�h�o��
	For nRow = Info.Header To Cursor.Rows.Count - 1
		For nColumn = 0 To Cursor.Columns.Count - 1
		
			Filter = Sheet.getCellByPosition(nColumn, Info.Filter).String
			IF Filter = "��" Then
				Stream.WriteText Sheet.getCellByPosition(nColumn, nRow).String + ","
			End IF
		Next nColumn
		Stream.WriteText "", 1
	Next nRow
	
	
  	'	�X�g���[���ɖ��O��t���ĕۑ�����(1�͐V�K�쐬 2�͏㏑���ۑ�)
  	Stream.SaveToFile FileName , 2

  	' �X�g���[�������
  	Stream.Close
  	Set Stream = Nothing
End Sub

' �O��������s����֐�
Sub CsvExport
	Dim Info As ExportInfo

	For Each Info In  GetExportInfo
		ExportByInfo(Info)
	Next Info

	MsgBox("�o�͂��܂���")
End Sub


