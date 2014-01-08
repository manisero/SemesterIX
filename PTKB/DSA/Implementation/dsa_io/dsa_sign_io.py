import re


class DSASignIO:
    def __init__(self):
        pass

    @staticmethod
    def export_sign_to_file(dsa_key, message, filename):
        with open(filename, 'w') as export_file:
            r, s = dsa_key.sign(message)
            export_file.write('({0},{1})\n'.format(r, s))
            export_file.write('{0}\n'.format(message))

    @staticmethod
    def verify_sign_from_file(dsa_key, filename):
        with open(filename, 'r') as import_file:
            import_file_contents = import_file.readlines()

            if len(import_file_contents) <= 1:
                raise IOError('Invalid file contents')

            match = re.search('\((\d+),(\d+)\)', import_file_contents[0].strip())

            if match is None or len(match.groups()) != 2:
                raise IOError('Invalid file contents')

            try:
                r, s = long(match.group(1)), long(match.group(2))
                message = '\n'.join([x.strip() for x in import_file_contents[1:]])
            except:
                raise IOError('Invalid file contents')

            return dsa_key.verify(message, r, s)
