class OutputWriter:
    def __init__(self):
        pass

    @staticmethod
    def write_graph(root_node, file_name):
        with open(file_name, 'w') as output_file:
            for node in root_node.iterator():
                output_file.write(str(node.node_id) + ' - ' + str(node.color) + '\n')
