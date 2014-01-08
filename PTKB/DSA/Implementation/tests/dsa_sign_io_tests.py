from tempfile import NamedTemporaryFile
import unittest
from mock import MagicMock
from dsa.dsa_key import DSAKey
from dsa_io.dsa_sign_io import DSASignIO


class DSASignIOTests(unittest.TestCase):
    def test_export_sign_to_file_method(self):
        with NamedTemporaryFile() as sign_file:
            message = 'This\nis\njust\na\ntest\nmessage'
            dsa_key = DSAKey()
            dsa_key.sign = MagicMock(return_value=(1, 2))
            DSASignIO.export_sign_to_file(dsa_key, message, sign_file.name)
            sign_file.seek(0)

            file_contents = sign_file.readlines()

            self.assertEqual(7, len(file_contents))
            self.assertEqual('(1,2)', file_contents[0].strip())
            self.assertEqual('This', file_contents[1].strip())
            self.assertEqual('is', file_contents[2].strip())
            self.assertEqual('just', file_contents[3].strip())
            self.assertEqual('a', file_contents[4].strip())
            self.assertEqual('test', file_contents[5].strip())
            self.assertEqual('message', file_contents[6].strip())

    def test_verify_sign_from_file_method(self):
        self.fail()
