   M S B A M L   `   `   `  ���� O  KComercial Estefannny, Version=1.0.0.1, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Comercial_Estefannny.ViewComercial_Estefannny.View  -    'Comercial_Estefannny.View.Transacciones   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     9local'clr-namespace:Comercial_Estefannny.View     X TXceed.Wpf.Toolkit, Version=4.6.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  .���   � RowDefinitions  �� $	��81* ��� $
��935* � � ColumnDefinitions �� ����  �� EndPoint$	 1,1&� �� 
StartPoint$	 0,0&�.���  � Color$ 	#FFFBABAD�� � Offset$ 0Q�� $ 	#FFFDCECF��$ 1Q�.+�&   System.Windows.Controls.DataGrid  -     Name$ dataGridVentasCompras��$��28,139,32,380q�   SelectionMode$ Single=�   
IsReadOnly . 	   AutoGenerateColumns	 .  
���   �$��1��$��Arial Black)�$��18(�
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 400 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *NombreEntidad+   ElementStyle ��  �� 
TargetType" ��.���  �� Property � �� �� Value$ Arial Black)���  � ��$ 18(�   HeaderStyle �� =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��$ Center=���  � ��$ Arial Black)���  � ��$ 18(�  $ Fecha��$	 200  �� *Fecha+ �� StringFormat$ {0:dd/MM/yyyy}�� �� " ��.���  � ��$ Arial Black)���  � ��$ 18(� �� "  .���  � ��$ Center=���  � ��$ Arial Black)���  � ��$ 18(�  $ Cantidad��$	 200  �� *TotalCantidad+ �� " ��.���  � ��$ Arial Black)���  � ��$ 18(� �� "  .���  � ��$ Center=���  � ��$ Arial Black)���  � ��$ 18(�  $ Total Antes del Descuento��$	 280  �� *TotalAntesDescuento+ �� " ��.���  � ��$ Arial Black)���  � ��$ 18(� �� "  .���  � ��$ Center=���  � ��$ Arial Black)���  � ��$ 18(�  $ 	Descuento��$	 200  �� *DescuentoTotal+ �� " ��.���  � ��$ Arial Black)���  � ��$ 18(� �� "  .���  � ��$ Center=���  � ��$ Arial Black)���  � ��$ 18(�  $ Total��$	 350  �� *
TotalVenta+ �� " ��.���  � ��$ Arial Black)���  � ��$ 18(� �� "  .���  � ��$ Center=���  � ��$ Arial Black)���  � ��$ 18(��� -    ��Name$ 
TextVentas��$
��Left=�$��
28,552,0,0q� �� TextWrapping$
 Wrap=�$��DETALLE��$	��Top=�$	��120��$��Arial Black)�$��20(�$��20��$��1����Z� ��Z� ��  �� Orientation$ 
Horizontal=�$
��Left=�$��28,39,0,844q�$��1��$	��310��.+�	� -    	�Name$ radioVentas��$��Ventas�� 	� 	GroupName$ VistaSeleccion�� d� 	IsChecked$
 TrueY�$��43��$��Arial Black)�$��20(�$��10q�$	��120��$��0,0,20,0q�	� -   $ radioCompras��$��Compras��$ VistaSeleccion��$��42��$��Arial Black)�$��20(�$��10q�$	��120���� $��Capital Invertido:��$��Arial Black)�$��20(�$
��Bold"�$	��Top=�$
��Left=�$��	65,24,0,0q�$��0��$	��236��$��30���� -    ��Name$ textBlockCapitalInvertido��$��Arial Black)�$��20(�$��288,0,1053,0q�$��0��$��22��$��Center=����� *CapitalInvertido+$ C���� $��	Ganancia:��$��Arial Black)�$��20(�$
��Bold"�$	��Top=�$
��Left=�$��
714,24,0,0q�$��0��$	��128��$��26�� J� RenderTransformOrigin$ 0.863,0.846&��� -   $ textBlockGanancia��$��Arial Black)�$��20(�$��847,21,467,0q�$��0��$��33��$	��Top=����� *
Ganancia+$ C���� $ 
Horizontal=�$��473,25,228,830q�$��1��.+��� $��Fecha Desde:��$��Arial Black)�$��20(�$��Center=�$
��Left=�$��0,0,10,0q�(  "System.Windows.Controls.DatePicker  -     Name$ dateTimePickerInicio��$��Arial Black)�$��20(�$��Center=�$
��Left=�$��10,0,0,0q��� $��Fecha Hasta:��$��Arial Black)�$��20(�$��Center=�$
��Left=�$��	30,0,10,0q�  -   $ dateTimePickerFin��$��Arial Black)�$��20(�$��Center=�$
��Left=�$��10,0,0,0q��� -	    ��Name$ detalleListView�� �� SelectionMode$ Single=�$��18(�$��Arial Black)�$��28,577,28,36q�$��1�� �� View �� .P��� $��Producto��  �� Width$	  500��! �� DisplayMemberBinding! �� *NombreProducto+�� $��Precio��$	  150��! �� *Precio+$ C���� $��	Descuento��$	  150��! �� *	Descuento+$ N2���� $��Cantidad��$	  150��! �� *
Cantidad+$ N0���� $��Variante��$	  150��! �� *
Variante+�� $��Total��$	  150��! �� *Total+$ C��