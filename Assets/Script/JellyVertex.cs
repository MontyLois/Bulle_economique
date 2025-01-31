using UnityEngine;

public class JellyVertex
{
    public int verticleIndex;
    public Vector3 initialVertexPosition;
    public Vector3 currentVertexPosition;
    
    public Vector3 currentVelocity;

    public JellyVertex(int _verticleIndex, Vector3 _initialVertexPosition, Vector3 _currentVertexPosition,
        Vector3 _currentVelocity)
    {
        verticleIndex = _verticleIndex;
        initialVertexPosition = _initialVertexPosition;
        currentVertexPosition = _currentVertexPosition;
        currentVelocity = _currentVelocity;
    }

    public Vector3 GetCurrentDisplacement()
    {
        return currentVertexPosition - initialVertexPosition;
    }

    public void UpdateVelocity(float _bounceSpeed)
    {
        currentVelocity = currentVelocity + GetCurrentDisplacement() * _bounceSpeed * Time.deltaTime;
    }

    public void Settle(float _stiffness)
    {
        currentVelocity *= 1f - _stiffness * Time.deltaTime;
    }

    public void ApplyPressureToVertex(Transform _transform, Vector3 _position, float _pressure)
    {
        Vector3 distanceVerticlePoint = currentVertexPosition - _transform.InverseTransformPoint(_position);
        float adaptedpressure = _pressure / (1f + distanceVerticlePoint.sqrMagnitude);
        float velocity = adaptedpressure * Time.deltaTime;
        currentVelocity += distanceVerticlePoint.normalized * velocity;
    }

}
