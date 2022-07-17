using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DigitalRuby.AnimatedLineRenderer
{
    public class DemoScriptPhasor : MonoBehaviour
    {
        public PhasorScript PhasorScript;
        public GameObject AsteroidPrefab;
        public AudioSource ExplosionSound;
        public ParticleSystem ExplosionParticleSystem;

        private readonly List<GameObject> asteroids = new List<GameObject>();

        private int score;

        private void CreateAsteroid()
        {
            GameObject clone = Instantiate(AsteroidPrefab);
            float scale = UnityEngine.Random.Range(0.5f, 2.0f);
            float rotation = UnityEngine.Random.Range(0.0f, 360.0f);
            clone.transform.localScale = new Vector3(scale, scale, 1.0f);
            clone.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);
            float x = UnityEngine.Random.Range(0.5f, 0.9f);
            float y = UnityEngine.Random.Range(0.1f, 0.9f);
            Vector3 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0.0f));
            worldPos.z = 0.0f;
            clone.transform.position = worldPos;

            float maxVelocity = 8.0f;
            clone.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(0.0f, maxVelocity), UnityEngine.Random.Range(0.0f, maxVelocity));
            clone.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(0.0f, 4.0f);

            asteroids.Add(clone);
        }

        private void DestroyAsteroid(GameObject asteroid)
        {
            if (asteroid != null)
            {
                ExplosionSound.PlayOneShot(ExplosionSound.clip);
                ExplosionParticleSystem.transform.position = asteroid.transform.position;
                short pieces = (short)Mathf.Max(8, (50.0f * asteroid.transform.localScale.x * asteroid.transform.localScale.x));
                ExplosionParticleSystem.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, pieces) }, 1);
                ExplosionParticleSystem.Play();
                GameObject.Destroy(asteroid);
            }
        }

        private void OnHit(RaycastHit2D[] hits)
        {
            foreach (RaycastHit2D hit in hits)
            {
                DestroyAsteroid(hit.collider.gameObject);
                score++;
            }
            GameObject label = GameObject.Find("ScoreLabel");
            label.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;
        }

        private void Start()
        {
            PhasorScript.HitCallback = OnHit;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0.0f;
                PhasorScript.Fire(pos);
            }

            for (int i = asteroids.Count - 1; i >= 0; i--)
            {
                GameObject obj = asteroids[i];
                if (obj == null || !obj.GetComponent<Renderer>().isVisible)
                {
                    GameObject.Destroy(obj);
                    asteroids.RemoveAt(i);
                }
            }

            if (UnityEngine.Random.Range(0, 50) == 5)
            {
                CreateAsteroid();
            }
        }
    }
}