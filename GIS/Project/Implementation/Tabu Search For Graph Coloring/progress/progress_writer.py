import time


class ProgressWriter:
    counter_default = -1

    def __init__(self, output, verbose=False):
        self.output = output
        self.verbose = verbose
        self.counters = {}

    def _start_counter(self, name):
        if name in self.counters and self.counters[name] != self.counter_default:
            raise ValueError('Restarting counter is not supported')

        self.counters[name] = time.time()

    def _stop_counter(self, name):
        if name not in self.counters or self.counters[name] == self.counter_default:
            raise ValueError('Could not stop counter which has not started')

        execution_time = time.time() - self.counters[name]
        self.counters[name] = self.counter_default

        return execution_time

    def _start_measure(self, code, name, verbose):
        self._start_counter(code)
        self.write_to_output('\n\nStarted {0} {1}...\n'.format(name, code), verbose)

    def _stop_measure(self, code, name, summary, verbose):
        execution_time = self._stop_counter(code)
        self.write_to_output('Finished {0} {1}!\nTime elapsed: {2} seconds\nSummary: {3}\n\n'.
                             format(name, code, str(execution_time), summary), verbose)

    def start_execution(self, name, verbose):
        self._start_measure('execution', name, verbose)

    def stop_execution(self, name, summary, verbose):
        self._stop_measure('execution', name, summary, verbose)

    def start_task(self, name, verbose):
        self._start_measure('task', name, verbose)

    def stop_task(self, name, summary, verbose):
        self._stop_measure('task', name, summary, verbose)

    def start_subtask(self, name, verbose):
        self._start_measure('sub-task', name, verbose)

    def stop_subtask(self, name, summary, verbose):
        self._stop_measure('sub-task', name, summary, verbose)

    def write_to_output(self, to_write, verbose=False):
        if verbose and not self.verbose:
            return

        if isinstance(self.output, str):
            with open(self.output, 'a') as output_file:
                output_file.write(to_write)
        else:
            self.output.write(to_write)
