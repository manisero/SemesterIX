import unittest
from dsa.dsa_key import DSAKey
from dsa_io.dsa_io import DSAKeyIO
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

    def test_import_key_method(self):
        self.fail()

