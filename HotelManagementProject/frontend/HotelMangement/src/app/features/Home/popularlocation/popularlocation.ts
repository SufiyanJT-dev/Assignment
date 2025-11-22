import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { text } from 'stream/consumers';

@Component({
  selector: 'app-popularlocation',
  imports: [CommonModule],
  templateUrl: './popularlocation.html',
  styleUrl: './popularlocation.scss',
})
export class Popularlocation {
 loactionsDeatils=[
  {
image:"https://storage.googleapis.com/uxpilot-auth.appspot.com/f409c2330e-304ee66696ec36d15cb8.png",
text:"Paris",
alt:"Paris skyline with Eiffel Tower, daytime, vibrant cityscape"
},{
 image:"https://storage.googleapis.com/uxpilot-auth.appspot.com/1d76048790-b539db1d6ca8b5113728.png",
text:"Tokyo",
alt:"Tokyo city street at night with neon lights, bustling with people"
},
{
image:"https://storage.googleapis.com/uxpilot-auth.appspot.com/3898dddfb8-5778a11f65f2553ed24c.png",
text:"Santorini",
alt:"Santorini, Greece with white buildings and blue domes overlooking the sea"
},
{
image:"https://storage.googleapis.com/uxpilot-auth.appspot.com/b4e2f0e0fd-678d898510b173b268ea.png",
text:"New York",
alt:"New York City skyline with Statue of Liberty, sunset"
},
{
  image:"https://storage.googleapis.com/uxpilot-auth.appspot.com/0bc2258003-106200db792d04ebfa6b.png",
text:"Sydney",
alt:"Sydney Opera House and Harbour Bridge, Australia, daytime"
}
,{
    image:"https://storage.googleapis.com/uxpilot-auth.appspot.com/7ffae401bb-91e1315d72465c971bf5.png",
text:"Rome",
alt:"Rome Colosseum, Italy, historical landmark, sunny day"
}
]

}

