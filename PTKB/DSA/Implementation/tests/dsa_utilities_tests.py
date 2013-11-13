import unittest
from dsa.dsa_utilities import DSAUtilities


class DSAUtilitiesTests(unittest.TestCase):
    def test_generate_random_p_bit_length_method(self):
        p_bit_length = DSAUtilities.generate_random_p_bit_length()

        self.assertTrue(p_bit_length >= 512)
        self.assertTrue(p_bit_length <= 1024)
        self.assertTrue((p_bit_length % 64) == 0)
