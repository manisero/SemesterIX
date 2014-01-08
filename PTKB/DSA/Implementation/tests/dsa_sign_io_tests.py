from tempfile import NamedTemporaryFile
import unittest
from mock import Mock
from dsa.dsa_key import DSAKey
from dsa_io.dsa_sign_io import DSASignIO


class DSASignIOTests(unittest.TestCase):
    def test_export_sign_to_file_method(self):
        with NamedTemporaryFile() as sign_file:
            message = 'This\nis\njust\na\ntest\nmessage'
            dsa_key = DSAKey()
            dsa_key.sign = Mock(return_value=(1, 2))
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
        with NamedTemporaryFile() as sign_file:
            message = 'Some message'
            sign = 4, 6
            dsa_key = DSAKey()
            dsa_key.verify = Mock(side_effect=lambda m, r, s: m == message and r == sign[0] and s == sign[1])
            sign_file.write('({0},{1})\n{2}\n'.format(sign[0], sign[1], message))
            sign_file.seek(0)

            verify_result = DSASignIO.verify_sign_from_file(dsa_key, sign_file.name)

            self.assertTrue(verify_result)

        with NamedTemporaryFile() as sign_file:
            message = 'Another piece of message'
            sign = 1, 2
            sign_file.write('({0},{1})\n{2}\n'.format(sign[0] + 1, sign[1] - 1, message))
            sign_file.seek(0)

            verify_result = DSASignIO.verify_sign_from_file(dsa_key, sign_file.name)

            self.assertFalse(verify_result)
