   M S B A M L   `   `   `  ����A  =Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Client.GUI.MainWindowsClient.GUI.MainWindows  5   	   '    !Client.GUI.MainWindows.ChatWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:Client.GUI.MainWindows     5   	     � Title$  
ChatWindow��5   	   $��384.213��6   $��413.415��6-    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�6.   .��5
      � .+�5   
   �� -   5         ��Name$ label��5      $��Choose Friend:��6   $
��Left=�68   $	��Top=�6S   $��	10,10,0,0q�6k   �� 5   
   -   5        $ 
label_Copy��5      $��	Messages:��6$   $
��Left=�68   $	��Top=�6S   $��
172,10,0,0q�6k   �� 5   
   -   5         ��Name$ 
lstMembers��5      $
��Left=�6&   $	��236��6A   $	��Top=�6N   $	��157��6f   $��	10,36,0,0q�6r   ���	#FF00FFF2���	#FF00F9F4��5      �� 5       �� EndPoint$ 0.5,1&�6&    � MappingMode$ RelativeToBoundingBox=�67    �� 
StartPoint$ 0.5,0&�6[   .��5      �  � Color$ Black��6#   	 � Offset$	 0Q�61   � 5      $ 	#FF00CDFF��6#   $	 0.535Q�65   � 5      $ 	#FF0083A3��6#   5      5      �� 5   
   -   5        $ lstMessages��5      $
��Left=�6'   $	��236��6B   $	��Top=�6O   $	��225��6g   $��
172,36,0,0q�6s   ��5      �� 5      $ 0.5,1&�6"   $ RelativeToBoundingBox=�63   $ 0.5,0&�6W   .��5      � $ Black��6   $	 0Q�6-   � 5      $ 	#FF00CDFF��6   $	 0.535Q�61   � 5      $ 	#FF0083A3��6   5      5      �� 5    
   -   5        
 ��Name$
 
txtMessage��5       $
��Left=�6&   $��57��6A    �� TextWrapping$
 Wrap=�6M   $	��Top=�6a   $	��239��6y   $��
10,277,0,0q�6�   ���	#FFCFF5FF���	#FFF1F2F46�   �� 5!   
   -   5         ��Name$ btnSend��5!      $
��Send��6"   $
��Left=�6G   $	��Top=�6b   $��75��6z   $��254,315,0,0q�6�   5#      5$      