   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Client.GUI
Client.GUI  5   	   7 clr-namespace:Client.DataClassesClient.DataClasses  5   	       Client.GUI.WelcomeWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Client.GUI     5   	   8DataClasses clr-namespace:Client.DataClasses     5   	     � Title$  Forum System��5	   	   $��320.2��6   $��411.2��6-    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�6.   .��5      � $	��Top=�6   $	��289��6#   $��	0,0,3.8,0q�60   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgForums��5         
IsReadOnly . $
��Left=�67   $	��Top=�6R   $	��187��6j   $	��285��6w   $��	10,74,0,0q�6�   �� 5   
   -   5         ��Name$ headlineLabel��5      $-��'           Welcome to the WASP system. ��6'   $
��Left=�6Y   $	��Top=�6t   $��	10,10,0,0q�6�   $��25��6�   $	��251��6�   �� 5   
   -   5         ��Name$ 
btnLoginSU��5      $��  login as 
SuperUser��6%   $
��Left=�6I   $	��Top=�6d   $��79��6|   $��
304,10,0,0q�6�   $��39��6�    � ToolTip$ Press to log in��6�   �� 5   
   -   5        $ 	btnLogOut��5      $��   log out 
(SuperUser)��6$   $
��Left=�6J   $	��Top=�6e   $��78��6}   $��
304,10,0,0q�6�   $��39��6�   $ Press to log out��6�   �� 5   
   -   5        $ btnNewForum��5      $��	New Forum��6&   $
��Left=�6:   $	��Top=�6U   $��79��6m   $��
304,74,0,0q�6x   $��31��6�   $ Press to add new Forum��6�   �� 5   
   -   5        $ 	btnDelete��5      $��Delete��6$   $
��Left=�65   $	��Top=�6P   $��79��6h   $��304,110,0,0q�6s   $��31��6�   $$ Press to delete selected forum��6�   �� 5   
   -   5        $ 
btnReports��5      $��Reports��6%   $
��Left=�68   $	��Top=�6S   $��79��6k   $��304,147,0,0q�6v   $��31��6�   $# Press to get system's reports��6�   �� 5   
   -   5        $ btnEnterForum��5      $��Enter Forum��6(   $
��Left=�6>   $	��Top=�6Y   $��79��6q   $��304,192,0,0q�6|   $��32��6�   $% Press to enter selescted forum.��6�   �� 5   
   -	   5        $ btnExit��5      $
��Exit��6"   $
��Left=�61   $	��Top=�6L   $��79��6d   $��304,229,0,0q�6o   $��32��6�   $! Press to exit forum system.��6�   5      5      