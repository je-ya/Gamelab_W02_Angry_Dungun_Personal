using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public ParticleSystem hitParticlePrefab;
    /*    private void OnTriggerEnter(Collider other)
        {
            // �浹�� ��ü���� Team ������Ʈ�� ������
            Aazz0200_Damager teamObject = other.GetComponent<Aazz0200_Damager>();

            if (teamObject.team == Team.Enemy)
            {
                // ��ƼŬ ȿ�� ����
                if (hitParticlePrefab != null)
                {
                    // �浹 �������� ��ƼŬ�� �ν��Ͻ�ȭ
                    ParticleSystem particle = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);

                    // ��ƼŬ ���
                    particle.Play();

                    // ��ƼŬ�� ������ �ڵ����� �����ǵ��� ���� (���� ����)
                    //Destroy(particle.gameObject, particle.main.duration);
                }
            }
        }*/
}

