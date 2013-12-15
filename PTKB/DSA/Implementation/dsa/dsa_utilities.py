from random import randrange, getrandbits


class DSAUtilities:
    def __init__(self):
        pass

    @staticmethod
    def generate_random_p_bit_length():
        return randrange(512, 1068, 64)
