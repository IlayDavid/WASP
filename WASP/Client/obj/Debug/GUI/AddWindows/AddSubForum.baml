   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Client.GUI
Client.GUI  5   	       Client.GUI.AddSubForum   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Client.GUI     5   	     � Title$  Add Sub-Forum��5   	   $��319.756��6   $��530.487��60    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�6.   .��5
      � .+�5   
   �� -   5         ��Name$ label��5      $��Name:��6   $
��Left=�6/   $	��Top=�6J   $��0,36,0,0q�6b   �� 5   
   -   5        $ 
label_Copy��5      $��Description:��6$   $
��Left=�6;   $	��Top=�6V   $��0,62,0,0q�6n   �� 5   
   -   5        $ label_Copy1��5      $��	Moderaor:��6%   $
��Left=�69   $	��Top=�6T   $��
270,34,0,0q�6l   �� 5   
   -   5        $ label_Copy2��5      $��Term:��6%   $
��Left=�65   $	��Top=�6P   $��
270,60,0,0q�6h   �� 5   
   -   5        $ label_Copy3��5      $��Moderaor Details:��6%   $
��Left=�6A   $	��Top=�6\   $��	270,8,0,0q�6t    J� RenderTransformOrigin$ 0.45,-0.795&�6�   $
��Bold"�6�   �� 5   
   -   5        $ label_Copy4��5      $��Sub-Forum Details:��6%   $
��Left=�6B   $	��Top=�6]   $��0,10,0,0q�6u   $
��Bold"�6�   �� 5   
   -   5         ��Name$ txtName��5      $
��Left=�6#   $��23��6>    �� TextWrapping$
 Wrap=�6J   $	��Top=�6^   $	��153��6v   $��	93,36,0,0q�6�   �� 5   
   -   5        $ txtDescription��5      $
��Left=�6*   $	��166��6E   $
 Wrap=�6R   $	��Top=�6f   $	��153��6~   $��	93,66,0,0q�6�   �� 5   
   -	   5         ��Name$ cboxModerator��5      $
��Left=�6*   $	��Top=�6E   $	��120��6]   $��
352,39,0,0q�6i    �� SelectedIndex$ 0��6}   .@�5      �� $��Select Moderator��6   5      &   System.Windows.Controls.Calendar  5   
   -
   5        	  Name$	 calTerm��5      $
��Left=�6$   $	��Top=�6?   $��
326,66,0,0q�6W   �� 5   
   -   5        
 ��Name$
 	btnCreate��5      $��Create��6$   $
��Left=�6M   $	��Top=�6h   $��75��6�   $��227,252,0,0q�6�   �� 5   
   
���   �$��0,0,3,0q�6%   $
��Left=�6?   $	��247��6Z   $	��Top=�6g   $��16��6   $��	253,0,0,0q�6�   5      5      