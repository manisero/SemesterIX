from dsa.dsa_key import DSAKey


class DSAKeyIO:
    def __init__(self):
        pass

    @staticmethod
    def export_key(dsa_key, filename):
        with open(filename, 'w') as export_file:
            if dsa_key.is_private():
                key_type = 'private'
            else:
                key_type = 'public'

            export_file.write('# {0} key\n'.format(key_type))
            export_file.write('{0}\n'.format(str(dsa_key.p)))
            export_file.write('{0}\n'.format(str(dsa_key.q)))
            export_file.write('{0}\n'.format(str(dsa_key.g)))
            export_file.write('{0}\n'.format(str(dsa_key.y)))

            if dsa_key.is_private():
                export_file.write('{0}\n'.format(str(dsa_key.x)))

    @staticmethod
    def import_key(filename):
        with open(filename, 'r') as import_file:
            import_file_contents = import_file.readlines()

            if len(import_file_contents) < 5:
                raise IOError('Invalid file contents')

            dsa_key = DSAKey()

            try:
                dsa_key.p = long(import_file_contents[1].strip())
                dsa_key.q = long(import_file_contents[2].strip())
                dsa_key.g = long(import_file_contents[3].strip())
                dsa_key.y = long(import_file_contents[4].strip())

                if len(import_file_contents) >= 6:
                    dsa_key.x = long(import_file_contents[5].strip())
            except:
                raise IOError('Invalid file contents')

            return dsa_key
