   M S B A M L   `   `   `  ����I  EPlatformy_Lab8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Platformy_Lab8Platformy_Lab8  5   	       Platformy_Lab8.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Platformy_Lab8     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � $��0,0,0,-6q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   o� $��0��6   .@�5      m� $
��File��6   .@�5      m� -   5        $
��Open��5      6)   m� 5      -   5        $
��Exit��5      6)   5      m� 5      -   5        $
��Save��5      6%   5      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      $��* �6#   .+�5!      R� -   5         R�Name$ treeView��5!      $��0��6)   ��5"      �� 5#       �� 
TargetType" Q�6   5%      5(      �� 5+      $��1��6   .��5,      �� -   5         ��Name$ fileContentTextBox��5,       �� TextWrapping$
 Wrap=�66    �� AcceptsReturn$
 True��6J    �� 
IsReadOnly$
 True��6_   5-      5.      �� 5/   
   $��2��6   $��Bottom=�6!   $��20��6<   $��0,0,0,5q�6H   .@�50      �� .��51      �� -   5        	 ��Name$	 statusTextBlock��51      $��Status��65   52      53      54      55      