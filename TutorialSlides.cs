using System.Collections.Generic;
using UnityEngine;

public class TutorialSlides: MonoBehaviour
{
    [SerializeField] private List<GameObject> _slides = new();
    private int _index = 0;

    private void OnEnable()
    {
        _index = 0;
        _slides[_index].SetActive(true);
    }

    public void NextSlide()
    {
        _slides[_index].SetActive(false);
        if (_index+1 < _slides.Count) _slides[_index + 1].SetActive(true);
        else gameObject.SetActive(false);
        _index++;
    }
}
