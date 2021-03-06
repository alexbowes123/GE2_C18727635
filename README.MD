# GE2-TopGunBattleAssignment


# Description 
- Alexander Bowes
- C18727635 
- TU856
- A recreation of the famous music video from the movie Top Gun. This video shows a fighter jet on a warship taking off and then battling enemy jets while the song Danger Zone plays.

# Video of Project
Video demo available here: https://www.youtube.com/watch?v=qJfphBaKNvQ
# Events Summary

1. Hero Jets takes off from Warship/ Music starts
2. Hero Jets gains altitude and perform movements above the ship
3. Follower jets will follow a leader Hero jet in an offset formation
4. Hero jets will gain altitude and fly above the clouds
5. Villain jets will enter the scene and begin chasing the Heroes
6. Villain jets will launch homing missiles at the Heroes when they are within a certain range
7. Missiles will spawn at the bottom of the jet, then us seek and arrive to find their targets.
8. When a jet is hit with a missile, they explode or fall out of the sky.
9. Hero jets will begin maneuvering away from missiles and attempt to take down the enemy jets.
10. Guns on the wayship may be implemented to shoot at enemy jets from the sea.
11. The Heroes will win and the remaining jets will land on the warship.
12. End

# STORYBOARD

1. Scene begins with hero jet preparing to take off.
![1-hero jet on ](/storyboard/1-take_off.png)
2. Hero jet flies up into the sky above the sea.
![1-hero jet on ](/storyboard/2-take_off.png)
3. Hero wanders through the sky.
![1-hero jet on ](/storyboard/3-leader_wanders.png)
4. Enemy jets spot the hero from a distance and 3 bad guys take off to chase the hero.
![1-hero jet on ](/storyboard/4-3_enemy_jets_take_off.png)
5. The enemies chase the hero in the sky.
![1-hero jet on ](/storyboard/5-3_enemies_chase_1_leader.png)
6. The enemies begin to shoot homing missiles at the hero.
![1-hero jet on ](/storyboard/6-miss.png)
7. Hero attempts to escape the enemies by gaining altitude and flying above clouds.
![1-hero jet on ](/storyboard/7-cloud.png)
8. More good guys and bad guys arrive and a full dog fight takes place.
![1-hero jet on ](/storyboard/8-dog.png)
9. FINAL - Heroes win and come in for a landing on the carrier.
![1-hero jet on ](/storyboard/final-landing.png)

# About music in project

The song used was found here: https://www.youtube.com/watch?v=4awuwMHtn54&t=149s

If the song does not sound like the video, please adjust the pitch at "Carrier_Hero_TakeOffCam_AudioSource" for the song.

# Most Proud of
I am most proud of the flare system which I implemented, despite it not working as intended. I think it is cool that I gave a jet a form of defence from homing missiles. I am also proud of the other models I created for submarines and artillery weapons which I unfortunately could not implement fully.


# States
FlightAttackState - The BadGuy Jets will target a hero jet and shoot missiles at them when in range.
Alive - Jets and Carriers have limited health, if their health falls under a certain limit then they will change to the dead state
Defending - When a jet receives damage from another jet's missile, the enemy is assigned and the victim enters the defending state to fight back

# Behaviours
Take Off - The jet follows a path that is relative to its base Carrier which goes along the run way and ascends
Wandering - Jets wander in the sky
ObstacleAviodance - ObstancleAvoidance is used by the hero when wandering to avoid other jets. This behaviour was modified to also detect missiles that come in range, if this happens then a flare is deployed to detract the missile from the hero.


# Models
The models currently being used will be replaced over time as the author creates new ones in Blender. As of week 6, the models used are taken from the asset store.

# Resources

## Models
https://assetstore.unity.com/packages/3d/vehicles/air/navy-planes-and-carrier-pack-170259

https://assetstore.unity.com/packages/2d/textures-materials/water/stylize-water-texture-153577


## Video for idea
https://www.youtube.com/watch?v=kUsFWO08CO0

https://www.youtube.com/watch?v=4awuwMHtn54&t=149s
