using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Utils
{
    public static TargetablePoint GetTargetPoint()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2000))
        {
            var targetable = hit.collider.transform.GetComponent<Targetable>();
            if (targetable != null)
            {
                return targetable.targetablePoints.OrderBy(tp => Vector3.Distance(hit.point, tp.transform.position)).First();
            }
        }
        return null;
    }
}
