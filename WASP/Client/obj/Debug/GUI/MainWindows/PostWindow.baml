   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	       Client.PostWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Client     5   	     � Title$  
PostWindow��5   	   $	��600��6   $��832.8��6)    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�6.   .��5
      � .+�5   
   �� -   5         ��Name$ welcomeTextBlock��5      $
��Left=�6.   $
��Wrap=�6I   $	��Top=�6]   $��
589,15,0,0q�6u   $��93��6�   $��Welcome, guest!��6�   �� 5   
   -   5         ��Name$ btnLogin��5      $��Log In��6#    � ToolTip$ Login to the forum��6K   $
��Left=�6h   $	��Top=�6�   $��54��6�   $��
697,10,0,0q�6�   $��26��6�   �� 5   
   -   5        $ 	btnLogout��5      $��Log Out��6$   $ Log out from the forum��6N   $
��Left=�6o   $	��Top=�6�   $��54��6�   $��
697,10,0,0q�6�   $��26��6�   �� 5   
   -   5        $ btnRegister��5      $��register��6&   $) #Register as new member to the forum��6S   $
��Left=�6�   $	��Top=�6�   $��61��6�   $��
756,10,0,0q�6�   $��26��6�   �� 5   
   -   5        $ refreshButton��5      $��Refresh��6(   $
��Left=�6:   $	��Top=�6U   $��75��6m   $��375,500,0,0q�6x   �� 5   
   -   5        $ 
exitButton��5      $
��Exit��6%   $ Exit from the system��64   $
��Left=�6S   $	��Top=�6n   $��75��6�   $��707,538,0,0q�6�   6�   �� 5   
   -   5         ��Name$ headlineLabel��5      $ ��Welcome to the WASP system��6'   $
��Left=�6L   $	��Top=�6g   $��
330,10,0,0q�6   �� 5   
   -   5        $ notificationsButton��5      $��Notifications(0)��6.   $
��Left=�6k   $	��Top=�6�   $��90��6�   $��
692,85,0,0q�6�   $" number of notifications is 0��6�   �� 5   
   -	   5        $ 
backButton��5      $
��back��6%   $, &Go back to sub forum threads selection��65   $
��Left=�6f   $	��Top=�6�   $��75��6�   $��
10,500,0,0q�6�   6�   � 5   
   -
   5         �Name$ dataGrid��5      $
��Left=�6!   $	��Top=�6<    J� RenderTransformOrigin$ 4.128,9.364&�6T   $	��450��6x   $	��676��6�   $��	10,45,0,0q�6�   
�������	 � RowDefinitions	 5      �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    
 � ColumnDefinitions
 5      �� 5      $	��300 �6#   .+�5!      �� -   5        $ btnAddReply��5!      $��	Add Reply��6*   $
��Left=�6X   $	��Top=�6s   $	��123��6�   $��50��6�   $��20(�6�   $��0��6�   $��	10,21,0,0q�6�   $$ Add reply to the selected post��6�   �� 5"      -   5        $ btnEdit��5"      $
��Edit��6&   $
��Left=�6K   $	��Top=�6f   $	��123��6   $��50��6�   $��20(�6�   $��1��6�   $��10,20.8,0,0q�6�   $ Edit the selected post��6�   R� 5#      -   5         R�Name$ postMesssages��5#      $
��Left=�6.   $	��397��6{   $	��Top=�6�   $	��480��6�   $��15(�6�   
�������$��169,18,-348.2,-59q�6�   $��5��6�   �� 5$      -   5        $ 	btnDelete��5$      $��Delete��6(   $
��Left=�6R   $	��Top=�6m   $	��123��6�   $��50��6�   $��20(�6�   $��2��6�   $��10,21.6,0,0q�6�   $��2��6�   $ Delete the selected post��6�   5%      5&      5'      