using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InputPresenter
{
    private InputView _inputView;
    private InputModel _inputModel;

    public InputPresenter(InputView inputView, InputModel inputModel)
    {
        _inputView = inputView;
        _inputModel = inputModel;
        Enable();
    }

    private void Enable()
    {
        _inputView.SendInput += SendInput;
    }

    private void SendInput(string input)
    {
        _inputModel.SendInput(input);
    }
}
