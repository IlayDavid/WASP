   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	       Client.ForumWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Client     5   	     � Title$  ForumWindow��5   	   $	��600��6   $��829.6��6+    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�6.   .��5
      � .+�5   
   �� -   5         ��Name$ welcomeTextBlock��5      $
��Left=�6.   $
��Wrap=�6I   $	��Top=�5      $��
589,15,0,0q�6,   $��93��6@   $��Welcome, guest!��6K   �� 5   
   -   5         ��Name$ refreshButton��5      $��Refresh��6(   $
��Left=�6:   $	��Top=�5      $��75��6)   $��375,500,0,0q�64   �� 5   
   -   5        $ 
exitButton��5      $
��Exit��6%   $
��Left=�64   $	��Top=�6O   $��75��6g   $��707,538,0,0q�6r   5      �� 5   
   -   5         ��Name$ headlineLabel��5      $ ��Welcome to the WASP system��6'   $
��Left=�6L   $	��Top=�6g   $��
240,10,0,0q�5      �� 5   
   -   5        $ notificationsButton��5      $��Notifications(0)��6.   $
��Left=�6I   $	��Top=�6d   $��91��6|   $��707,174,0,0q�5       � ToolTip$" number of notifications is 0��6&   $��38��6o   �� 5   
   -   5        $ 
backButton��5      $
��back��6%   $+ %Go back to the forum selection window��6M   $
��Left=�5      $	��Top=�6,   $��75��6D   $��
10,500,0,0q�6O   � 5   
   -   5         �Name$ dataGrid��5      $
��Left=�6!   $	��Top=�6<    J� RenderTransformOrigin$ 4.128,9.364&�6T   $	��450��6x   $	��676��6�   $��	10,45,0,0q�6�   
�������	 � RowDefinitions	 5      �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    �� 5       $��70 �6    �� 5!      $��70 �6    
 � ColumnDefinitions
 5#      �� 5$      $	��301 �6#   .+�5&      �� -   5        $ btnViewMembers��5&      $��View Members��6-   $  View members of this forum��6D   $
��Left=�6�   $	��Top=�6�   $	��202��6�   $��50��6�   $��20(�6�   $��	10,10,0,0q�6�   �� 5'      -	   5        $ btnSendMessage��5'      $��Send Message��6-   $) #Send Message to member in the forum��6D   $
��Left=�5(   2   $	��Top=�6M   $	��202��6e   $��50��6q   $��20(�6}   $��	10,10,0,0q�5)      $��1��6(   �� 5*      -
   5        $ btnAddAdministrator��5*      $��Add Administrator��62   $  Add new admin to the forum��6N   $
��Left=�6�   $	��Top=�6�   $	��202��6�   $��50��6�   $��20(�6�   $��	10,10,0,0q�6�   $��2��6  �� 5+      -   5        $ btnAddSubforum��5+      $��Add Subforum��6-   $$ Add new Sub Forum to the forum��6D   $
��Left=�6�   $	��Top=�6�   $	��202��6�   $��50��6�   $��20(�6�   $��3��6�   $��10,8,0,0q�6�   �� 5,      -   5        $ btnEditForumPolicy��5,      $��Change Policy��61   $$ Change the policy of the forum��6I   $
��Left=�6�   $	��Top=�6�   $	��202��6�   $��50��6�   $��20(�6�   $��4��6�   $��	10,10,0,0q�6�   �� 5-      -   5         ��Name$ 	SubForums��5-       �� SelectionMode$ Single=�6)   $
��Left=�6@   $	��397��6[   $	��Top=�6h   $	��325��6�   $��15(�6�   
�������$��324,18,-347,-131q�6�   $��4��6�   6�   �� 5.      -   5        $ 
btnReports��5.      $��Reports��6)   $ Forum reports��6;   $
��Left=�6l   $	��Top=�6�   $	��202��6�   $��50��6�   $��20(�6�   $��5��6�   $��10,9,0,0q�6�   5/      �� 50   
   -   5        $ btnLogin��50      $��Log In��6#   $ Login to the forum��6K   $
��Left=�6h   $	��Top=�6�   $��54��6�   $��
697,10,0,0q�6�   $��26��6�   �� 51   
   -   5        $ 	btnLogout��51      $��Log Out��6$   $ Log out from the forum��6N   $
��Left=�6o   $	��Top=�6�   $��54��6�   $��
697,10,0,0q�6�   $��26��6�   �� 52   
   -   5        $ btnRegister��52      $��register��6&   $) #Register as new member to the forum��6S   $
��Left=�6�   $	��Top=�6�   $��61��6�   $��
756,10,0,0q�6�   $��26��6�   53      54      