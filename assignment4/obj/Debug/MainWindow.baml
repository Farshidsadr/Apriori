   M S B A M L   `   `   `  ����  assignment4    assignment4.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Apriori��5   	   $	��350��6   $	��525��6&   $��Times new roman)�62   $��20(�6O    � 
ResizeMode$ NoResize=�6]    � WindowStartupLocation$ CenterScreen=�5   	    � Icon 5      �� 5   
    �� 	UriSource$ images/y (2).png?�6   65   .��5	      � 
�������.+�5
   
   �� $��images/apriori2.png��6   $��302,12,37.4,245.8q�6-   �� 5   
   -   5        $	��Top=�5   %   $
��Left=�6=   $��36��6X   $	��150��6d   $��
147,27,0,0q�5      $��Select csv file��6%   
����d�6?   �� 5   
   $��Max support��6   $��25,90,376.4,195.8q�6'   �� 5   
   -   5        $��TextBox��5      $��30��6!   $	��Top=�6-   $
��Left=�6E   $��
147,87,0,0q�6`   $	��150��6t   �� 5   
   -   5        $��Label��5      $	��Top=�6   $
��Left=�65   $��17(�6P   $��30��6^   $��
25,130,0,0q�6j   $	��124��6~   6�   �� 5   
   -   5        $��Buttonstart��5      $��30��6$   $	��150��60   $��0,87,49.4,0q�6<   $��Start��6Q   
����d�$��Right=�5      $	��Top=�6-   6a   &   System.Windows.Controls.DataGrid  5   
   -   5        $��DataGrid��5         CanUserAddRows .     AutoGenerateColumns . $
��Left=�6U   $��
25,175,0,0q�6p   $	��Top=�5      $	��136��6+   $	��450��68      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ItemSet��69   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 300 6J   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6%   �� 6X   5      5      5      