import unittest
from dsa.dsa_key import DSAKey
from dsa_io.dsa_key_io import DSAKeyIO
from tempfile import NamedTemporaryFile


class DSAKeyIOTests(unittest.TestCase):
    def test_export_private_key_method(self):
        with NamedTemporaryFile() as key_file:
            dsa_key = DSAKey()
            dsa_key.p = 1
            dsa_key.q = 3
            dsa_key.g = 5
            dsa_key.y = 6
            dsa_key.x = 7

            DSAKeyIO.export_key(dsa_key, key_file.name)

            key_file.seek(0)
            key_file_contents = key_file.readlines()

            self.assertEqual(6, len(key_file_contents))
            self.assertEqual('# private key', key_file_contents[0].strip())
            self.assertEqual('1', key_file_contents[1].strip())
            self.assertEqual('3', key_file_contents[2].strip())
            self.assertEqual('5', key_file_contents[3].strip())
            self.assertEqual('6', key_file_contents[4].strip())
            self.assertEqual('7', key_file_contents[5].strip())

    def test_export_public_key_method(self):
        with NamedTemporaryFile() as key_file:
            dsa_key = DSAKey()
            dsa_key.p = 6
            dsa_key.q = 0
            dsa_key.g = 9
            dsa_key.y = 6

            DSAKeyIO.export_key(dsa_key, key_file.name)

            key_file.seek(0)
            key_file_contents = key_file.readlines()

            self.assertEqual(5, len(key_file_contents))
            self.assertEqual('# public key', key_file_contents[0].strip())
            self.assertEqual('6', key_file_contents[1].strip())
            self.assertEqual('0', key_file_contents[2].strip())
            self.assertEqual('9', key_file_contents[3].strip())
            self.assertEqual('6', key_file_contents[4].strip())

    def test_import_public_key_method(self):
        with NamedTemporaryFile() as key_file:
            key_file.write('# public key\n')
            key_file.write('5\n')
            key_file.write('10\n')
            key_file.write('15\n')
            key_file.write('20\n')
            key_file.seek(0)

            dsa_key = DSAKeyIO.import_key(key_file.name)

            self.assertFalse(dsa_key.is_private())
            self.assertEqual(5, dsa_key.p)
            self.assertEqual(10, dsa_key.q)
            self.assertEqual(15, dsa_key.g)
            self.assertEqual(20, dsa_key.y)
            self.assertIsNone(dsa_key.x)

    def test_import_private_key_method(self):
        with NamedTemporaryFile() as key_file:
            key_file.write('# private key\n')
            key_file.write('0\n')
            key_file.write('1\n')
            key_file.write('2\n')
            key_file.write('3\n')
            key_file.write('4\n')
            key_file.seek(0)

            dsa_key = DSAKeyIO.import_key(key_file.name)

            self.assertTrue(dsa_key.is_private())
            self.assertEqual(0, dsa_key.p)
            self.assertEqual(1, dsa_key.q)
            self.assertEqual(2, dsa_key.g)
            self.assertEqual(3, dsa_key.y)
            self.assertEqual(4, dsa_key.x)

