   M S B A M L   `   `   `  ���� O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  *    $Comercial_Estefannny.View.Inventario   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Comercial_Estefannny.View     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .��� .+��   � RowDefinitions  �� $	��51* ��� $	��50* � � ColumnDefinitions �� $��* �.+�� $��1�� �� .+��� -   $��ProductosListView��$��10,23,10,10q�$��Visible=�$��Arial Black)�$��11(�$��Visible=� �� View �� .P��� $��Nombre Producto�� �� Width$	 150�� �� DisplayMemberBinding �� *NombreProducto+�� $��
Categoría��$	 100�� �� *NombreCategoria+�� $��Marca��$	 100�� �� *NombreMarca+�� $��Variante��$	 100�� �� *
Variante+�� $��Precio Compra��$	 100�� �� *PrecioCompra+�� $��Precio Venta��$	 100�� �� *PrecioVenta+�� $��Cantidad��$	 100�� �� *
Cantidad+�� $��Código Barras��$	 100�� �� *CodigoBarras+�� -    ��Name$ TextBuscador��$
��Left=�$��10,0,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��171��$��18��� $��	0,1,375,0q�  �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� -   $ 	TextMarca��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��MARCA:��$��Center=�$��92��$��Arial Black)�$��16(� J� 	AllowDrop .  �� 
IsReadOnly . $��20����Z� ��Z� 	 �� SelectionBrush	 Z� �� -   $ TextProducto��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��	PRODUCTO:��$��Center=�$	��118��$��Arial Black)�$��16(� .  . $��2��$��20����Z� ��Z� 	 Z� �� -   $ TextCategoria��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��
CATEGORIA:��$��Center=�$	��118��$��Arial Black)�$��16(� .  . $��1��$��20����Z� ��Z� 	 Z� �� -   $ TextCodigoBarra��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��CODIGO BARRA:��$��Center=�$	��152��$��Arial Black)�$��16(� .  . $��3��$��20����Z� ��Z� 	 Z� �� -   $ TextVariante��$
��Left=�$��	10,12,0,0q�$
 Wrap=�$��	VARIANTE:��$	��Top=�$	��118��$��Arial Black)�$��16(� .  . $��4��$��20����Z� ��Z� 	 Z� �� -   
 ��Name$
 MarcaComboBox��$
��Left=�$��Center=�$	��147��$��	206,0,0,0q� �� 
IsEditable .  �� IsTextSearchEnabled .   �� StaysOpenOnEdit . $��Disabled=��� -	   $
 CategoriaComboBox��$
��Left=�$��Center=�$	��147��$��	206,0,0,0q� . $��1���� -
   $
 ProductoComboBox��$
��Left=�$	��147��$��	206,0,0,0q� .  .   . $��2��$��22��$��Center=��� -   $
 VarianteComboBox��$
��Left=�$	��Top=�$	��147��$��
206,12,0,0q� .  .  . $��4���� -   $ TextPrecioCompra��$
��Left=�$��	10,10,0,0q�$
 Wrap=�$��PRECIO COMPRA:��$	��Top=�$	��178��$��Arial Black)�$��16(� .  . $��5��$��20����Z� ��Z� 	 Z� �� -   $ TextPrecioVenta��$
��Left=�$��10,0,0,0q�$
 Wrap=�$��PRECIO VENTA:��$��Center=�$	��164��$��Arial Black)�$��16(� .  . $��6��$��20����Z� ��Z� 	 Z� �� $
��Left=�$��0��$��
210,43,0,0q�$��5��$
 Wrap=�$��TextBox��$	��Top=�$	��120��%  Xceed.Wpf.Toolkit.DecimalUpDown  -     Name$ NumericPrecioCompra��$
��Left=�$��
206,10,0,0q�$��5��$	��Top=�$	��147��  -   $ NumericPrecioVenta��$
��Left=�$��
206,10,0,0q�$��6��$	��Top=�$	��147���� -   $ TextCodigoBarra1��$
��Left=�$��	206,0,0,0q�$��3��$
 Wrap=�$��Center=�$	��147��$��22��� $��
376,0,-1,1q�  �� $��0* ��� .+��� -    ��Name$ BotonAgregarProducto��$��Agregar��$
��Left=�$��	29,10,0,0q�$��2��$	��Top=�$	��148��$��Arial Black)�$��32��
���   ��� -   $ BotonEliminarProducto��$��Eliminar��$
��Left=�$��	29,57,0,0q�$��2��$	��Top=�$	��148��
���   �$��32��$��Arial Black)��� -   $ BotonEditarProducto��$��Editar��$
��Left=�$��
29,103,0,0q�$��2��$	��Top=�$	��148��
���   �$��31��$��Arial Black)��� -   $ BotonImportarDatos��$��Importar Datos��$
��Left=�$��
29,152,0,0q�$��2��$	��Top=�$	��148��
���   �$��32��$��Arial Black)��� -   $ BotonEscanearCodigo��$��Escanear Codigo��$
��Left=�$��
29,199,0,0q�$��2��$	��Top=�$	��148��
���   �$��32��$��Arial Black)��� $��Capital Invertido:��$��Arial Black)�$��20(�$
��Bold"�$	��Top=�$
��Left=�$��
290,24,0,0q�$��0��$	��236��$��30��$��2���� -    ��Name$ textBlockCapitalInvertido��$��Arial Black)�$��20(�$��503,19,202,0q�$��0��$��40��$	��Top=�$��2������ *CapitalInvertido+ �� StringFormat$ C������  �� EndPoint$	 1,1&� �� 
StartPoint$	 0,0&�.���  � Color$ 	#FFFBABAD�� � Offset$ 0Q�� $ 	#FFFDCECF��$ 1Q�