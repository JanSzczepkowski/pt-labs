   M S B A M L   `   `   `  ����  Platformy_Lab8(    "Platformy_Lab8.FileNameInputWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Enter File Name��5   	   $	��300��6!   $	��300��6.   .��5      �  � RowDefinitions 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   .+�5      �� $��
File Name:��6
   $��0��6   $��5q�6,   �� 5      -   5         ��Name$ fileNameTextBox��5      $��1��6%   $��5q�62   �� 5       �� Orientation$ 
Horizontal=�6   $��2��6(   $��5q�65   .+�5      	� -   5         	�Name$ fileRadioButton��5       $
��File��6   6    	� 5      -   5        $ pathRadioButton��5       $
��Path��6   $��10,0,0,0q�69   5      �� 5      $��DOS Attributes��6   $��3��6%   $��5q�62   .��5      �� .+�5      �� -   5         ��Name$ readOnlyCheckBox��5   #   $��	Read-only��6   $��5q�6=   �� 5      -   5        $ archiveCheckBox��5   !   $��Archive��6   $��5q�6:   �� 5      -   5        $ systemCheckBox��5       $��System��6   $��5q�68   �� 5      -   5        $ hiddenCheckBox��5       $��Hidden��6   $��5q�68   5      5      �� 5      $ 
Horizontal=�6   $��Center=�6(   $��4��6E   $��5q�6R   .+�5       �� -   5        $��OK��5       $��75��6   $��5q�6$   6/   �� 5!      -	   5        $��Cancel��5!      $��75��6   $��5q�6(   63   5"      5#      5$      