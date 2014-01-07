import unittest
from dsa.dsa_key import DSAKey
from random import getrandbits


class DSAKeyTests(unittest.TestCase):
    def test_positive_workflow(self):
        dsa_key = DSAKey.generate_instance(2048)
        private_key = dsa_key.get_private_key()
        public_key = dsa_key.get_public_key()

        r, s = private_key.sign('Test message')
        validity = public_key.verify('Test message', r, s)

        self.assertTrue(validity)

    def test_changed_letter_message(self):
        dsa_key = DSAKey.generate_instance(2048)
        private_key = dsa_key.get_private_key()
        public_key = dsa_key.get_public_key()

        r, s = private_key.sign('Test message')
        validity = public_key.verify('Best message', r, s)

        self.assertFalse(validity)

    def test_changed_order_message(self):
        dsa_key = DSAKey.generate_instance(2048)
        private_key = dsa_key.get_private_key()
        public_key = dsa_key.get_public_key()

        r, s = private_key.sign('Test message')
        validity = public_key.verify('Tset message', r, s)

        self.assertFalse(validity)

    def test_changed_length_message(self):
        dsa_key = DSAKey.generate_instance(2048)
        private_key = dsa_key.get_private_key()
        public_key = dsa_key.get_public_key()

        r, s = private_key.sign('Test message')
        validity = public_key.verify('message', r, s)

        self.assertFalse(validity)

    def test_random_r_key(self):
        dsa_key = DSAKey.generate_instance(2048)
        private_key = dsa_key.get_private_key()
        public_key = dsa_key.get_public_key()

        r, s = private_key.sign('Test message')
        random_r = getrandbits(r.bit_length())
        validity = public_key.verify('Test message', random_r, s)

        self.assertEqual(random_r == r, validity)

    def test_random_s_key(self):
        dsa_key = DSAKey.generate_instance(2048)
        private_key = dsa_key.get_private_key()
        public_key = dsa_key.get_public_key()

        r, s = private_key.sign('Test message')
        random_s = getrandbits(s.bit_length())
        validity = public_key.verify('Test message', r, random_s)

        self.assertEqual(random_s == s, validity)

    def test_random_keys(self):
        dsa_key = DSAKey.generate_instance(2048)
        private_key = dsa_key.get_private_key()
        public_key = dsa_key.get_public_key()

        r, s = private_key.sign('Test message')
        random_r, random_s = getrandbits(r.bit_length()), getrandbits(s.bit_length())
        validity = public_key.verify('Test message', random_r, random_s)

        self.assertEqual(random_r == r and random_s == s, validity)
