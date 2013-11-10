class ColoringValidator:
    def __init__(self):
        pass

    @staticmethod
    def is_coloring_valid(root_node):
        for node in root_node.iterator():
            for child_node in node.edges:
                if node.color == child_node.color:
                    return False

        return True
