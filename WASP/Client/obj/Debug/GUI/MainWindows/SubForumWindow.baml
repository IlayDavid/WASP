   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ClientClient  5   	       Client.SubForumWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Client     5   	     � Title$  SubForumWindow��5   	   $	��600��6    $��851.6��6-    � WindowStartupLocation$ CenterScreen=�5	   	    � 
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
��Exit��6%   $
��Left=�64   $	��Top=�6O   $��75��6g   $��707,538,0,0q�6r   6�   �� 5   
   -   5         ��Name$ headlineLabel��5      $ ��Welcome to the WASP system��6'   $
��Left=�6L   $	��Top=�6g   $��
330,10,0,0q�6   �� 5   
   -   5        $ notificationsButton��5      $��Notifications(0)��6.   $
��Left=�6k   $	��Top=�6�   $��90��6�   $��
692,85,0,0q�6�   $" number of notifications is 0��6�   �� 5   
   -	   5        $ 
backButton��5      $
��back��6%   $+ %Go back to sub-forum selection window��64   $
��Left=�6d   $	��Top=�6   $��75��6�   $��
10,500,0,0q�6�   6�   � 5   
   -
   5         �Name$ dataGrid��5      $
��Left=�6!   $	��Top=�6<    J� RenderTransformOrigin$ 4.128,9.364&�6T   $	��450��6x   $	��676��6�   $��	10,45,0,0q�6�   
�������	 � RowDefinitions	 5      �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    �� 5      $��70 �6    
 � ColumnDefinitions
 5      �� 5       $	��300 �6#   .+�5"      �� -   5        $ btnPostThread��5"      $��
Add Thread��6,   $ Add thread to sub forum��6A   $
��Left=�6   $	��Top=�6�   $	��183��6�   $��50��6�   $��20(�6�   $��0��6�   $��10,8,0,0q�6�   �� 5#      -   5        $ btnViewModerators��5#      $��View Moderators��60   $+ %View moderators list of the sub forum��6J   $
��Left=�6�   $	��Top=�6�   $	��183��6�   $��50��6�   $��20(�6�   $��
10,7.8,0,0q�6�   $��1��6  �� 5$      -   5        $ btnAddModerator��5$      $��Add Moderator��6/   $  Add moderator to sub forum��6G   $
��Left=�6�   $	��Top=�6�   $	��183��6�   $��50��6�   $��20(�6�   $��2��6�   $��
10,7.6,0,0q�6�   �� 5%      -   5        $ btnEditModeratorTerm��5%      $��Edit Mod. Term��64   $ Edit moderator term��6N   $
��Left=�6�   $	��Top=�6�   $	��183��6�   $��50��6�   $��20(�6�   $��3��6�   $��
10,8.4,0,0q�6�   �� 5&      -   5        $ btnRemoveModerator��5&      $��Remove Moderator��61   $( "Remove moderator from the subforum��6M   $
��Left=�6�   $	��Top=�6�   $	��183��6�   $��50��6�   $��20(�6�   $��4��6�   $��
10,8.2,0,0q�6  �� 5'      -   5        $ 	btnRepots��5'      $��Reports��6(   $" Get report for the sub forum��6:   $
��Left=�6y   $	��Top=�6�   $	��183��6�   $��50��6�   $��20(�6�   $��5��6�   $��10,8,0,0q�6�   �� 5(      -   5         ��Name$ SubForumsThreads��5(       �� SelectionMode$ Single=�60   $
��Left=�6G   $	��397��6b   $	��Top=�6o   $	��325��6�   $��15(�6�   
�������$��324,18,-347,-131q�6�   $��4��6�   6�   5)      5*      5+      