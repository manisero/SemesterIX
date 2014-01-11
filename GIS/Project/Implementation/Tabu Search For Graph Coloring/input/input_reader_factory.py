from input.dimacs_input_reader import DIMACSInputReader
from input.input_reader import InputReader


class InputReaderFactory:
    def __init__(self):
        pass

    @staticmethod
    def create_input_reader(dimacs_compatibility):
        if dimacs_compatibility:
            return DIMACSInputReader()

        return InputReader()
