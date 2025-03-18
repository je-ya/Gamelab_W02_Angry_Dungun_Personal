using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public ParticleSystem hitParticlePrefab;
    /*    private void OnTriggerEnter(Collider other)
        {
            // 충돌한 객체에서 Team 컴포넌트를 가져옴
            Aazz0200_Damager teamObject = other.GetComponent<Aazz0200_Damager>();

            if (teamObject.team == Team.Enemy)
            {
                // 파티클 효과 생성
                if (hitParticlePrefab != null)
                {
                    // 충돌 지점에서 파티클을 인스턴스화
                    ParticleSystem particle = Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);

                    // 파티클 재생
                    particle.Play();

                    // 파티클이 끝나면 자동으로 삭제되도록 설정 (선택 사항)
                    //Destroy(particle.gameObject, particle.main.duration);
                }
            }
        }*/
}

