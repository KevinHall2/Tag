using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TagSystem : MonoBehaviour
{
   
    [SerializeField]
    private bool _startTagged = false;

    [SerializeField]
    private float _tagImmunityDuration = 1.0f;

    [SerializeField]
    private GameObject _tagParticlesPrefab;

    private bool _tagged = false;
    private bool _tagImmune = false;

    public bool Tagged { get { return _tagged; } }

    public bool Tag()
    {
        //if already tagged, do nothing
        if (Tagged)
        {
            return false;
        }

        //if immune to tag, do nothing
        if (_tagImmune)
        {
            return false;
        }
        _tagged = true;
        SpawnParticles();
        return true;
    }

    private void SpawnParticles()
    {
        if (!_tagParticlesPrefab)
        {
            return;
        }

        GameObject obj = Instantiate(_tagParticlesPrefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    private void SetTagImmuneFalse()
    {
        _tagImmune = false;
    }

    private void Start()
    {
        _tagged = _startTagged;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Tagged)
            return;

        if (collision.gameObject.TryGetComponent(out TagSystem tagSystem))
        {
            if (tagSystem.Tag())
            {
                _tagged = false;
                _tagImmune = true;
                Invoke(nameof(SetTagImmuneFalse), _tagImmunityDuration);
            }
        }
    }
}
