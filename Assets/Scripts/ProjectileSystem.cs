using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSystem
{
    public delegate Vector3 DelegateProjectileSystem(Vector3 target, Vector3 origin, float time);
    public DelegateProjectileSystem delegateProjectileSystem;

    public ProjectileSystem()
    {
        delegateProjectileSystem += CalculateVelocity;
    }
    private Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time) // Using Projectile Motion Formula to calculate the one object's projectile to the other,(parabolic arc)
    {
        //Defining the distance X and Y
        Vector3 distance = target - origin; // Finding the distance X,Z between target and the player
        Vector3 distanceXZ = distance; // Assigning the distance X,Z value only to a new vector
        distanceXZ.y = 0f; // Defining the Y value of the same vector as zero

        // Creating floats which represents distance, for using in the velocity
        float sY = distance.y; // The original distance Y  
        float sXZ = distanceXZ.magnitude; // The original distance X,Z but with length of the vector only

        float vXZ = sXZ / time; // To find velocity on X,Z plane, distance X,Z divided by the time
        float vY = sY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time; // To find the velocity Y at origin or zero

        Vector3 results = distanceXZ.normalized; // The final Vector,Normalizing the distance X,Z vector
        results *= vXZ; // The final vector is product of final vector into distance of X,Z 
        results.y = vY + 0.5f; // But the Y value is equals to the above calculated

        return results;
    }
}
