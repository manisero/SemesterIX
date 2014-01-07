import unittest
from dsa.dsa_key import DSAKey
from random import getrandbits


class DSAKeyTests(unittest.TestCase):
    def test_positive_workflow(self):
        dsa_key = DSAKey.generate_instance(2048)

        r, s = dsa_key.sign('Test message')
        validity = dsa_key.verify('Test message', r, s)

        self.assertTrue(validity)

    def test_changed_message(self):
        dsa_key = DSAKey.generate_instance(2048)

        r, s = dsa_key.sign('Test message')
        validity = dsa_key.verify('Best message', r, s)

        self.assertFalse(validity)

    def test_random_key(self):
        dsa_key = DSAKey.generate_instance(2048)

        r, s = dsa_key.sign('Test message')
        random_r, random_s = getrandbits(r.bit_length()), getrandbits(s.bit_length())
        validity = dsa_key.verify('Test message', random_r, random_s)

        self.assertEqual(random_r == r and random_s == s, validity)
