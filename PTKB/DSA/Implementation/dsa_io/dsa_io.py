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
        pass
