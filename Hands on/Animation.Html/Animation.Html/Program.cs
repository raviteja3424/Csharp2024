﻿ < !DOCTYPE html >
 

 < html lang = "en" xmlns = "http://www.w3.org/1999/xhtml" >
    < head >
    
        < meta charset = "utf-8" />
     
         < title ></ title >
     
         < style >
             div{
width: 100px;
height: 100px;
    background - color:red;
    animation - name:test;
    animation - duration:600ms;
position: relative;
}
/*To have the animation effect using keyframes*/
@keyframes test
{
            0%{
        background - color:red;
    }
            25%{
        background - color:yellow;
    left: 200px;
    top: 0px;
    }
            50% {
        background - color: blue;
    left: 200px;
    top: 200px;
    }
            
            100%{
        background - color:green;
    }
}
    </ style >
</ head >
< body >
    < div > Hello </ div >
</ body >
</ html >