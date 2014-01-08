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
        pass
