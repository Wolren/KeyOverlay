   M S B A M L   `   `   `  ����  Programowanie)    #Programowanie.Overlays.MouseOverlay   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	     � Title$  MouseOverlay��5   	   $	��300��6   $	��490��6+    � WindowStyle$
 None=�67    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�6   
������  � AllowsTransparency$
 True��5	   "    � Topmost$
 True��6<   $��0q�5
   	   .��5      � .+�5   
   �� $��300, 10, 0, 0q�6   $	��CPS��6+   
�������$��20(�6I    �� TextAlignment$ Center=�6W   �� 5   
   -   5        $	��Cps��5   +   $��300, 40, 0, 0q�6   
�������$ Center=�6I   �� 5   
   $��300, 90, 0, 0q�6   $��CLICKS��6+   
�������$��20(�6L   $ Center=�6Z   �� 5   
   -   5        $��Clicks��5   ,   $��300, 120, 0, 0q�6   
�������$ Center=�6M   �� 5   
   $��300, 170, 0, 0q�6   $��SCROLLS��6,   
�������$��20(�6N   $ Center=�6\   �� 5   
   -   5        $��Scroll��5   ,   $��300, 200, 0, 0q�6   
�������$ Center=�6M   �� 5   
   $	��Top=�6   $
��Left=�6)    � 	Resources 5       �� 
TargetType	 �� Property
 �� Value��%�  '��       '�� &    �� 5      " ��6   .�5      �� 	 � ��
 �#A1B6C863   �� 5      	 �@��TextAlignment$
 Center=�66   �� 5      	 � ��$	
 100��6.   �� 5      	 � ��$
 52��6/   �� 5      	 � ��$
 16(�61   �� 5      	 �@��Padding$
 0, 16, 0, 0q�60   5      �� 5      " ��6   .�5      �� 	 �@��CornerRadius$
 4��65   �� 5      	 � ��$
 3q�68   �� 5      	 � ��

 �   �64   �� 5       	 � ��$
 0q�6/   5!      5"      .+�5#      �� $��10, 50, 0, 0q�6   ._�5$      �� -   5        $��LC��5$      $��
Left Click��6&   5%      �� 5&      $��120, 50, 0, 0q�6   ._�5'      �� -   5        $��SW��5'      $��Scroll Wheel��6&   5(      �� 5)      $��230, 50, 0, 0q�6   ._�5*      �� -   5        $��RC��5*      $��Right Click��6&   5+      5,      5-      5.      