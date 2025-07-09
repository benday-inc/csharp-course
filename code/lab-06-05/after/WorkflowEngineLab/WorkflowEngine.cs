using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkflowEngineLab
{
    public delegate Task WorkflowStepAsync();

    public class WorkflowEngine
    {
        public event EventHandler<string>? StepStarted;
        public event EventHandler<string>? StepCompleted;
        public event EventHandler<string>? StepFailed;

        private List<(string Name, WorkflowStepAsync Step)> steps = new();

        public void AddStep(string name, WorkflowStepAsync step)
        {
            steps.Add((name, step));
        }

        public void AddConditionalStep(string name, WorkflowStepAsync step, Func<bool> condition)
        {
            if (condition())
            {
                steps.Add((name, step));
            }
        }

        public async Task ExecuteAsync()
        {
            foreach (var (name, step) in steps)
            {
                StepStarted?.Invoke(this, name);
                try
                {
                    await step();
                    StepCompleted?.Invoke(this, name);
                }
                catch
                {
                    StepFailed?.Invoke(this, name);
                }
            }
        }
    }
}
