   M S B A M L   `   `   `  ����  Platformy_Lab11     Platformy_Lab11.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Newton Symbol Calculator��5   	   $	��560��6*   $	��400��67   .��5      � $��	0,0,0,-16q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��Enter N:��6   $��0��6#   $��10q�60   �� 5   
   -   5         ��Name$ InputN��5      $��0��6"   $��10q�6/   $	��100��6;   $��Center=�6G   �� 5   
   $��Enter K:��6   $��1��6#   $��10q�60   �� 5   
   -   5        $ InputK��5      $��1��6"   $��10q�6/   $	��100��6;   $��Center=�6G   �� 5   
   $��Fibbonacci number��6   $��2��6,   $��10q�69   �� 5   
   -   5         ��Name$ StartButton��5      $��2��6&   $��Start��63   $	��100��6C   $��30��6O   $��10q�6[   $��Center=�6g   6�   � 5   
   -   5         �Name$ ProgressBar��5      $��3��6+   $��30��68   $��10q�6D    � Minimum$ 0Q�6P    � Maximum$	 100Q�6\   �� 5   
   -   5        $ 
TaskButton��5      $��4��6%   $��Start with Task��62   $	��150��6L   $��30��6X   $��10q�6d   $��Center=�6p   6�   �� 5   
   -   5        $ DelegateButton��5      $��5��6)   $��Start with Delegate��66   $	��150��6T   $��30��6`   $��10q�6l   $��Center=�6x    J� RenderTransformOrigin$ 
0.49,1.961&�6�   �� 5    
   -   5        $ AsyncAwaitButton��5       $��6��6+   $��Start with Async-Await��68   $	��150��6Y   $��30��6e   $��	0,10,0,65q�6q   $��Center=�6�   $ 0.547,-1.209&�6�   �� 5"   
   -   5        $ CompressButton��5"      $��7��6)   $��Compress Files��66   $	��150��6O   $��30��6[   $��	0,19,0,60q�6g   $��Center=�6z   6�   �� 5#   
   -	   5        $ DecompressButton��5#      $��6��6+   $��Decompress Files��68   $	��150��6S   $��30��6_   $��	0,65,0,10q�6k   $��Center=�6~   $ 0.519,-0.833&�6�   �� 5%   
   -
   5        $ ResultTextBox��5%      $��9��6)   $��
10,72,10,7q�66   $��30��6J   "	 �� VerticalScrollBarVisibility$
	 Auto=�6V   
 �� 
IsReadOnly$

 True��6y   5&      5'      